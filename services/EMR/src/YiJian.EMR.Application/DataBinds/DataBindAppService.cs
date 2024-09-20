using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Linq.Dynamic.Core;
using YiJian.EMR.Libs.Dto;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Enums;
using Volo.Abp.Uow;
using Volo.Abp;
using YiJian.EMR.Libs.Entities;
using YiJian.EMR.Enums;
using Microsoft.EntityFrameworkCore;
using YiJian.EMR.DataBinds.Contracts;
using YiJian.EMR.DataBinds.Dto;
using YiJian.EMR.DataBinds.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace YiJian.EMR.DataBinds
{
    /// <summary>
    /// 数据绑定服务
    /// </summary>
    [Authorize]
    public class DataBindAppService : EMRAppService, IDataBindAppService
    {
        private readonly IDataBindContextRepository _dataBindContextRepository;
        private readonly IDataBindMapRepository _dataBindMapRepository;
        public DataBindAppService(
            IDataBindContextRepository dataBindContextRepository,
            IDataBindMapRepository dataBindMapRepository
        )
        {
            _dataBindContextRepository = dataBindContextRepository;
            _dataBindMapRepository = dataBindMapRepository;
        }

        /// <summary>
        /// 获取绑定
        /// </summary>
        /// <param name="patientEmrId"></param>
        /// <returns></returns>
        public async Task<ResponseBase<Dictionary<string, Dictionary<string, string>>>> GetBindAsync(Guid patientEmrId)
        {
            var map = new Dictionary<string, string>();
            var entity = await _dataBindContextRepository.GetDetailAsync(patientEmrId);
            if (entity == null) return new ResponseBase<Dictionary<string, Dictionary<string, string>>>(EStatusCode.CNULL);
             
            var ret = new Dictionary<string, Dictionary<string, string>>();
            var list = entity.DataBindMaps;
            var group = list.GroupBy(g=>g.DataSource).OrderBy(o=>o.Key).ToList();
            foreach (var datasource in group)
            {
                var item = new Dictionary<string, string>();
                foreach (var data in datasource)
                { 
                    item.Add(data.Path,data.Value);
                }
                ret.Add(datasource.Key,item);
            }

            return new ResponseBase<Dictionary<string, Dictionary<string, string>>>(EStatusCode.COK,ret);
        }

        /// <summary>
        /// 修改或新增绑定的电子病历，文书数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>  
        [UnitOfWork]
        public async Task<ResponseBase<Guid>> ModifyBindAsync(ModifyDataBindDto model)
        { 
            var entity = await (await _dataBindContextRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == model.PatientEmrId);
            if (entity == null)
            {
                var newEntity = new DataBindContext(
                    id: GuidGenerator.Create(),
                    visitNo: model.VisitNo,
                    orgCode: model.OrgCode, 
                    registerSerialNo: model.RegisterSerialNo,
                    pi_id: model.PI_ID,
                    patientId: model.PatientId,
                    patientName: model.PatientName,
                    writerId: model.WriterId,
                    writerName: model.WriterName,
                    classify: model.Classify,
                    patientEmrId: model.PatientEmrId.Value);
                var retEntity = await _dataBindContextRepository.InsertAsync(newEntity, autoSave: true);
                entity = retEntity;
            }
            else
            {
                entity.Update(
                    visitNo: model.VisitNo,
                    orgCode: model.OrgCode, 
                    registerSerialNo: model.RegisterSerialNo,
                    patientId: model.PatientId,
                    patientName: model.PatientName,
                    writerId: model.WriterId,
                    writerName: model.WriterName,
                    classify: model.Classify);
                _ = await _dataBindContextRepository.UpdateAsync(entity, autoSave: true);
            }

            Dictionary<string,Dictionary<string,string>> data = new();
            foreach (var item in model.Data)
            {
                if (data.ContainsKey(item.Key)) continue;
                Dictionary<string, string> dic = new();
                foreach (var otem in item.Value)
                {
                    if (dic.ContainsKey(otem.Key)) continue;

                    if (otem.Value is string)
                    {
                        dic.Add(otem.Key, otem.Value as string);
                    }
                    else
                    {
                        dic.Add(otem.Key, JsonConvert.SerializeObject(otem.Value));
                    } 
                }
                if (dic.Count>0) data.Add(item.Key,dic); 
            }

            await _dataBindMapRepository.BatchUpdateAsync(entity.Id, data);
            return new ResponseBase<Guid>(EStatusCode.COK,entity.Id);
        }
     
    }
}
