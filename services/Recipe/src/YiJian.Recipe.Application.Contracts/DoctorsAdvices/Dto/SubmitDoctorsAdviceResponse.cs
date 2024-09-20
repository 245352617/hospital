using System;
using System.Collections.Generic;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 提交订单返回的记录信息
    /// </summary>
    public class SubmitDoctorsAdviceResponse
    {
        /// <summary>
        /// 医嘱id集合
        /// </summary>
        public List<Guid> Ids { get; set; } = new List<Guid>();

        /// <summary>
        /// 单据信息，用于打印
        /// </summary>
        public List<PushDoctorsAdviceModel> Orders { get; set; } = new List<PushDoctorsAdviceModel>();

    }

}
