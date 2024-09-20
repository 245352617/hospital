using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 时间轴Dto
    /// </summary>
    public class TimeAxisRecordDto
    {
        /// <summary>
        /// 患者分诊信息Id
        /// </summary>
        public Guid PI_ID { get; set; }


        /// <summary>
        /// 时间点编码
        /// </summary>
        public string TimePointCode { get; set; }

        /// <summary>
        /// 时间点名称
        /// </summary>
        public string TimePointName { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}