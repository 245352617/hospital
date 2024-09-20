using System;
using System.Text.Json.Serialization;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.Nursing
{
    /// <summary>
    /// 查询插管列表入参
    /// </summary>
    public class NursingCanulaInput
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 使用标志：（Y在用，N已拔管）
        /// </summary>
        public string UseFlag { get; set; }

        /// <summary>
        /// 状态：（0插管时间，1拔管时间）
        /// </summary>
        public int? State { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int VisitStatus { get; set; }

        /// <summary>
        /// 出科时间
        /// </summary>
        [JsonConverter(typeof(DatetimeJsonConvert))]
        public DateTime? OutDeptTime { get; set; }
    }
}