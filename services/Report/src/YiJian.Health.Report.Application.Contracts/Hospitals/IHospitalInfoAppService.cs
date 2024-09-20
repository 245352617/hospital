using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.Hospitals.Dto;

namespace YiJian.Health.Report.Hospitals
{
    /// <summary>
    /// 医院信息
    /// </summary>
    public interface IHospitalInfoAppService : IApplicationService
    {
        /// <summary>
        /// 获取医院的护理单信息
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<HospitalInfoDto>> GetAsync();

        /// <summary>
        /// 添加/更新
        /// </summary>
        /// <see cref="ModifyHospitalDto"/>
        /// <returns></returns>
        public Task<ResponseBase<HospitalInfoDto>> ModifyAsync(ModifyHospitalDto model);

        /// <summary>
        /// 删除医院信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> DeleteAsync(Guid id);

    }
}
