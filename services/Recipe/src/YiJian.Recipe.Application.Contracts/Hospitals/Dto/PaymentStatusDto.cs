using System;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 支付状态
    /// </summary>
    public class PaymentStatusDto : EntityDto<Guid>
    {
        /// <summary>
        /// 支付状态
        /// </summary>
        public EPayStatus PayStatus { get; set; }

    }


}
