using DotNetCore.CAP;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Volo.Abp;
using Volo.Abp.Data;
using Volo.Abp.Uow;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Permissions;
using YiJian.EMR.Writes.Dto;
using YiJian.EMR.Writes.Entities;

namespace YiJian.EMR.Writes
{
    /// <summary>
    /// 书写病历总线部分
    /// </summary>
    public partial class WriteAppService
    {
        #region 交接班数据

        /// <summary>
        /// 获取一天内的患者交接班的电子病历
        /// </summary>
        /// <param name="model">交接班请求参数，如果begintime ,endtime 不传过来则获取当前患者当天的所有电子病历</param>
        /// <returns></returns>
        public async Task<ResponseBase<List<PatientEmrDto>>> GetPatientShiftChangeEmrsAsync(ShiftChangeRequestDto model)
        {
            var query = await (await _patientEmrRepository.GetQueryableAsync())
                .Where(w => w.PatientNo == model.PatientNo)
                .WhereIf(model.BeginTime.HasValue && model.EndTime.HasValue,
                    w => w.CreationTime >= model.BeginTime.Value.Date &&
                         w.CreationTime < model.EndTime.Value.Date.AddDays(1))
                .WhereIf(!model.BeginTime.HasValue && !model.EndTime.HasValue,
                    w => w.CreationTime >= DateTime.Now.Date && w.CreationTime < DateTime.Now.Date.AddDays(1))
                .WhereIf(model.BeginTime.HasValue && !model.EndTime.HasValue,
                    w => w.CreationTime >= model.BeginTime.Value.Date)
                .WhereIf(!model.BeginTime.HasValue && model.EndTime.HasValue,
                    w => w.CreationTime < model.EndTime.Value.Date.AddDays(1))
                .ToListAsync();

            var data = ObjectMapper.Map<List<PatientEmr>, List<PatientEmrDto>>(query);
            return new ResponseBase<List<PatientEmrDto>>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 获取指定患者电子病历的汇总数据
        /// </summary>
        /// <returns></returns> 
        [AllowAnonymous]
        public async Task<ResponseBase<EmrCategoryDto>> GetAggregatePatientShiftChangeEmrAsync(
            EmrCategoryRequestDto model)
        {
            var data = new EmrCategoryDto();

            //指定查询抢救，心肺复苏数据
            var qiangJiu = "QiangJiu";
            var xinFeiFuSu = "XinFeiFuSu";

            var query = await (await _patientEmrRepository.GetQueryableAsync())
                .Where(w => w.DoctorCode == model.DoctorCode && w.CategoryLv1 == qiangJiu &&
                            w.CategoryLv2 == xinFeiFuSu)
                .WhereIf(model.BeginTime.HasValue && model.EndTime.HasValue,
                    w => w.CreationTime >= model.BeginTime.Value.Date &&
                         w.CreationTime < model.EndTime.Value.Date.AddDays(1))
                .WhereIf(model.BeginTime.HasValue, w => w.CreationTime >= model.BeginTime.Value.Date)
                .WhereIf(model.EndTime.HasValue, w => w.CreationTime < model.EndTime.Value.Date.AddDays(1))
                .Select(s => new { s.PatientNo, s.CategoryLv1, s.CategoryLv2 })
                .ToListAsync();

            var values = new List<string>() { qiangJiu, xinFeiFuSu };
            var categories = await (await _categoryPropertyRepository.GetQueryableAsync()).Where(w => values.Contains(w.Value)).ToListAsync();

            if (query.Count == 0)
            {
                var qjEntity = categories.FirstOrDefault(w => w.Value == qiangJiu);
                var xffsEntity = categories.FirstOrDefault(w => w.Value == xinFeiFuSu);
                data.QiangJiu = new EmrCategoryCountDto { Value = qjEntity.Value, Label = qjEntity.Label, Count = 0 };
                data.XinFeiFuSu = new EmrCategoryCountDto
                { Value = xffsEntity.Value, Label = xffsEntity.Label, Count = 0 };
            }

            var c1 = query
                .Where(w => w.CategoryLv1 == qiangJiu)
                .Select(s => new { s.PatientNo, s.CategoryLv1 })
                .Distinct();

            if (c1.Any())
            {
                var first = c1.FirstOrDefault();
                var count = c1.Count();
                var category = await (await _categoryPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Label == first.CategoryLv1);
                data.QiangJiu = new EmrCategoryCountDto
                { Value = category.Value, Label = category.Label, Count = count };
            }

            var c2 = query
                .Where(w => w.CategoryLv2 == xinFeiFuSu)
                .Select(s => new { s.PatientNo, s.CategoryLv2 })
                .Distinct();

            if (c2.Any())
            {
                var first = c2.FirstOrDefault();
                var count = c2.Count();
                var category = await (await _categoryPropertyRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Label == first.CategoryLv2);
                data.XinFeiFuSu = new EmrCategoryCountDto
                { Value = category.Value, Label = category.Label, Count = count };
            }

            return new ResponseBase<EmrCategoryDto>(EStatusCode.COK, data);
        }

        #endregion


        #region 提供给定时服务处理

        /// <summary>
        /// 获取电子病历，护理文书的的id集合
        /// </summary> 
        /// <returns></returns> 
        [AllowAnonymous]
        public async Task<List<PatientEmrSampleDto>> GetArchivePatientEmrsAsync()
        {
            return await (await _patientEmrRepository.GetQueryableAsync())
                .Where(w => !w.IsArchived && (w.DischargeTime.HasValue && DateTime.Now.AddHours(-72) >= w.DischargeTime.Value))
                .Select(s => new PatientEmrSampleDto
                {
                    Id = s.Id,
                    Classify = s.Classify,
                    PatientNo = s.PatientNo,
                    PatientName = s.PatientName,
                    Title = s.EmrTitle
                })
                .ToListAsync();
        }

        /// <summary>
        /// 归档
        /// </summary>
        /// <param name="patientEmrId">电子病历、文书id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [UnitOfWork]
        public async Task<string> ArchiveAsync(Guid patientEmrId)
        {
            var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == patientEmrId);
            if (entity == null) return "";
            entity.Archive();
            await _patientEmrRepository.UpdateAsync(entity);

            var xml = await (await _patientEmrXmlRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.PatientEmrId == patientEmrId);
            if (xml == null) return "";
            return xml.EmrXml;
        }

        /// <summary>
        /// 异常回滚归档
        /// </summary>
        /// <param name="patientEmrId">电子病历、文书id</param>
        /// <returns></returns>
        [AllowAnonymous]
        [UnitOfWork]
        public async Task RollbackArchiveAsync(Guid patientEmrId)
        {
            var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == patientEmrId);
            if (entity == null) return;
            entity.Archive(false);
            await _patientEmrRepository.UpdateAsync(entity, true);
        }

        #endregion


        #region 全景视图-患者病历列表

        /// <summary>
        /// 全景视图-患者病历列表
        /// </summary>
        /// <param name="PID"></param>
        /// <param name="StartTime"></param>
        /// <param name="EndTime"></param> 
        /// <returns></returns>
        [Obsolete]
        public async Task<List<PatientEmrDto>> GetAllViewPatientEmrListAsync(Guid PID, DateTime? StartTime, DateTime? EndTime)
        {

            try
            {
                var patientEmrList = await (await _patientEmrRepository.GetQueryableAsync())
                .Where(w => w.PI_ID == PID)
                //.WhereIf(!Convert.IsDBNull(request.StartTime), w => w.CreationTime >= Convert.ToDateTime(request.StartTime))
                //.WhereIf(!Convert.IsDBNull(request.EndTime), w => w.CreationTime <= Convert.ToDateTime(request.EndTime))
                .WhereIf(StartTime.HasValue, w => w.CreationTime >= Convert.ToDateTime(StartTime))
                .WhereIf(EndTime.HasValue, w => w.CreationTime <= Convert.ToDateTime(EndTime))
                .OrderBy(o => o.CreationTime)
                .ToListAsync();

                var list = ObjectMapper.Map<List<PatientEmr>, List<PatientEmrDto>>(patientEmrList);
                return list;

                //return RespUtil.Ok<List<PatientEmrDto>>(data: list);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return null;
                //return RespUtil.InternalError<List<PatientEmrDto>>(extra: ex.Message);
            }
        }

        #endregion

        #region 电子病历水印配置

        /// <summary>
        /// 获取电子病历水印配置
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous] 
        public ResponseBase<EmrWatermarkModel> GetWatermark()
        {
            try
            {
                return new ResponseBase<EmrWatermarkModel>(EStatusCode.COK, _emrWatermarkModel.CurrentValue); 
            }
            catch (Exception ex)
            {
                _logger.LogError($"获取电子病历水印配置错误:{ex}");
                return new ResponseBase<EmrWatermarkModel>(EStatusCode.COK, new EmrWatermarkModel { Enabled = false });
            }
        }

        #endregion

        #region 电子病历垃圾桶

        /// <summary>
        /// 获取删除的患者电子病历记录列表
        /// </summary>
        /// <param name="doctorCode"></param>
        /// <param name="piid"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[Authorize(EMRPermissions.Writes.List)]
        public async Task<ResponseBase<IList<PatientEmrTrashDto>>> GetPatientEmrTrashAsync(string doctorCode, Guid piid)
        {
            using (_dataFilter.Disable<ISoftDelete>())
            { 
                var query = (await _patientEmrRepository.GetQueryableAsync())
                   .Where(w => w.PI_ID == piid)
                   .Where(w => w.IsDeleted)
                   .WhereIf(!doctorCode.IsNullOrEmpty(), w => w.DoctorCode == doctorCode.Trim())
                   .OrderByDescending(o => o.DeletionTime)
                   .Select(s => new PatientEmrTrashDto
                   {
                       Id = s.Id,
                       PI_ID = s.PI_ID,
                       Classify = s.Classify,
                       DoctorCode = s.DoctorCode,
                       DoctorName = s.DoctorName,
                       PatientName = s.PatientName,
                       PatientNo = s.PatientNo,
                       Title = s.EmrTitle,
                       IsDeleted = s.IsDeleted,
                       DeletionTime = s.DeletionTime, 
                   }); 
                var list = await query.ToListAsync();
                return await Task.FromResult( new ResponseBase<IList<PatientEmrTrashDto>>(EStatusCode.COK, list));
            }
        }

        /// <summary>
        /// 还原垃圾桶里的电子病历
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorCode"></param>
        /// <returns></returns>
        //[AllowAnonymous]
        //[Authorize(EMRPermissions.Writes.Modify)]
        public async Task<ResponseBase<bool>> RestorePatientEmrAsync(Guid id, string doctorCode)
        {
            if (doctorCode.IsNullOrEmpty()) return new ResponseBase<bool>(EStatusCode.CFail, false, "未知操作人，不能执行还原电子病历操作！");

            using (_dataFilter.Disable<ISoftDelete>())
            {
                try
                {
                    var entity = await (await _patientEmrRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == id);
                    if (entity.DoctorCode != doctorCode) return new ResponseBase<bool>(EStatusCode.CFail, false, "你不能还原别人的电子病历！");

                    entity.IsDeleted = false;
                    _ = await _patientEmrRepository.UpdateAsync(entity);
                    return new ResponseBase<bool>(EStatusCode.COK, true);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"还原垃圾桶电子病历异常：{ex.Message}");
                    return new ResponseBase<bool>(EStatusCode.COK, false);
                }
            }
        }

        #endregion

        #region 电子病历可合并的模板

        /// <summary>
        /// 获取可以合并的电子病历模板源
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        //[Authorize(EMRPermissions.Writes.Detail)]
        public async Task<ResponseBase<List<string>>> GetMergeEmrOriginalIdsAsync()
        {
            //try
            //{
            //    var mergeEmrs = _configuration.GetSection("Merge_EmrPatientEmr_OriginalId").Value;
            //    var arr = mergeEmrs.Split(';', '|').Select(s => s.ToLower()).ToList();
            //    return new ResponseBase<List<string>>(EStatusCode.COK,data:arr); 
            //}
            //catch (Exception ex)
            //{  
            //    _logger.LogError(ex,$"获取合并电子病历模板异常：{ex.Message}");
            //    return new ResponseBase<List<string>>();
            //}

            var list = await _mergeTemplateWhiteListRepository.GetListAsync();
            var data = list.Select(s=>s.TemplateId.ToString()).ToList(); 
            return new ResponseBase<List<string>>(EStatusCode.COK,data:data);
        }

        #endregion


        #region 运维工具

        /// <summary>
        /// 运维工具 只能执行一次执行之后不要随意执行（不要随意执行，数据采集功能）
        /// </summary>
        /// <returns></returns> 
        [Obsolete]
        private async Task GatherAllMinioEmrAsync()
        {
            var emrList = await _minioEmrInfoRepository.GetEmrDataAsync(true);
            var urls = emrList.Select(s => s.PatientEmrId).ToArray(); 
           
            if (urls.Any())
            {
                List<MinioEmrInfo> list = new List<MinioEmrInfo>(); 
                var data = await GetUrlsAsync(urls);
                foreach (var item in data)
                {
                    var emr = emrList.FirstOrDefault(w => w.PatientEmrId == item.Key);
                    if (emr != null)
                    {
                        var info = new MinioEmrInfo(GuidGenerator.Create(), patientEmrId: emr.PatientEmrId, emrTitle: emr.EmrTitle, minioUrl: item.Value, pI_ID: emr.PI_ID, patientNo: emr.PatientNo, patientName: emr.PatientName, doctorCode: emr.DoctorCode, doctorName: emr.DoctorName);
                        list.Add(info);
                    }
                }
                await _minioEmrInfoRepository.AddAsync(list);
            }
        }

        #endregion



    }
}
