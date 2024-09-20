using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Hospitals.Contracts;
using YiJian.Health.Report.Hospitals.Dto;
using YiJian.Health.Report.Hospitals.Entities;

namespace YiJian.Health.Report.Hospitals
{
    /// <summary>
    /// 医院信息
    /// </summary>
    [Authorize]
    public class HospitalInfoAppService : ReportAppService, IHospitalInfoAppService
    {
        private readonly IHospitalInfoRepository _hospitalInfoRepository;

        public HospitalInfoAppService(
            IHospitalInfoRepository hospitalInfoRepository
        )
        {
            _hospitalInfoRepository = hospitalInfoRepository;
        }

        /// <summary>
        /// 获取医院的护理单信息
        /// </summary>
        /// <returns></returns> 
        public async Task<ResponseBase<HospitalInfoDto>> GetAsync()
        {
            var entity = await (await _hospitalInfoRepository.GetQueryableAsync()).OrderByDescending(o => o.CreationTime).FirstOrDefaultAsync();
            if (entity == null) return new ResponseBase<HospitalInfoDto>(EStatusCode.CNULL);
            var data = ObjectMapper.Map<HospitalInfo, HospitalInfoDto>(entity);
            return new ResponseBase<HospitalInfoDto>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 添加/更新
        /// </summary>
        /// <see cref="ModifyHospitalDto"/>
        /// <returns></returns>
        public async Task<ResponseBase<HospitalInfoDto>> ModifyAsync(ModifyHospitalDto model)
        {
            if (model.Id.HasValue)
            {
                var entity = await (await _hospitalInfoRepository.GetQueryableAsync()).FirstOrDefaultAsync(w => w.Id == model.Id.Value);
                if (entity == null) return new ResponseBase<HospitalInfoDto>(EStatusCode.CNULL);
                entity.Update(model.Name, model.Logo, model.HospitalLevel, model.Address);
                var data = await _hospitalInfoRepository.UpdateAsync(entity);
                var map = ObjectMapper.Map<HospitalInfo, HospitalInfoDto>(data);
                return new ResponseBase<HospitalInfoDto>(EStatusCode.COK, map);
            }
            else
            {
                var entity = new HospitalInfo(GuidGenerator.Create(), model.Name, model.Logo, model.HospitalLevel, model.Address);
                var data = await _hospitalInfoRepository.InsertAsync(entity);
                var map = ObjectMapper.Map<HospitalInfo, HospitalInfoDto>(data);
                return new ResponseBase<HospitalInfoDto>(EStatusCode.COK, map);
            }
        }

        /// <summary>
        /// 删除医院信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<Guid>> DeleteAsync(Guid id)
        {
            await _hospitalInfoRepository.DeleteAsync(w => w.Id == id);
            return new ResponseBase<Guid>(EStatusCode.COK, id);
        }

    }
}
