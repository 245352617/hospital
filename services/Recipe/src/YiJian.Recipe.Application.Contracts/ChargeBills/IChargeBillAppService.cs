using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ChargeBills.Dto;

namespace YiJian.ChargeBills
{
    /// <summary>
    /// 收费记录单
    /// </summary>
    public interface IChargeBillAppService : IApplicationService
    {
        /// <summary>
        /// 院前收费单
        /// </summary>
        /// <param name="piid"></param>
        /// <returns></returns>
        public Task<PreBillDto> PreBillAsync(Guid piid);



    }
}
