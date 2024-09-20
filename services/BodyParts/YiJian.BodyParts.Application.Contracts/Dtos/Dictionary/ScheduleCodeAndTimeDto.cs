using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 班次代码和班次时间
    /// </summary>
    public class ScheduleCodeAndTimeDto
    {
        /// <summary>
        /// 班次代码
        /// </summary>
        /// <example></example>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        /// <example></example>
        public DateTime EndTime { get; set; }
    }
}
