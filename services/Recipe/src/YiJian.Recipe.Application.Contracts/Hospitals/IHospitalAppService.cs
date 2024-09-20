using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.Hospitals.Dto;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 医院接口
    /// </summary>
    public interface IHospitalAppService : IApplicationService
    {
        /// <summary>
        /// 医嘱支付状态变更
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<bool> PaymentStatusChangeAsync(PaymentStatusDto model);

        /// <summary>
        /// 推送医嘱状态信息
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<bool> UpdateRecordStatusAsync(List<UpdateRecordStatusDto> model);


    }
}
