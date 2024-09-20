using System;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 电子病历分类汇总记录请求参数
    /// </summary>
    public class EmrCategoryRequestDto
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get;set;}

    }

}
