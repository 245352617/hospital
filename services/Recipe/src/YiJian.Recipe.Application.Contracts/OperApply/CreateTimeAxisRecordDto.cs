using System;

namespace YiJian.Recipe
{
    /// <summary>
    /// 时间轴Dto
    /// </summary>
    public class CreateTimeAxisRecordDto
    {
        /// <summary>
        /// 患者分诊信息Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 时间点编码
        /// </summary>
        public int TimePointCode { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}