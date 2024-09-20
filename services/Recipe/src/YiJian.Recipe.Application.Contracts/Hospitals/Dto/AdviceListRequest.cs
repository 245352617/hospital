using System;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 请求医嘱列表参数
    /// </summary>
    public class AdviceListRequest
    {
        /// <summary>
        /// 患者唯一ID
        /// </summary>
        public Guid PIID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginDate { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// 未使用过的医嘱（忽视时间段，对全部医嘱过滤）
        /// </summary>
        public bool UnusedAdvice { get; set; } = false;

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 开启附加项，默认是不开启的，需要前端传参控制
        /// </summary>
        public bool? OpenAdditionalTreat { get; set; } = false;

    }

}