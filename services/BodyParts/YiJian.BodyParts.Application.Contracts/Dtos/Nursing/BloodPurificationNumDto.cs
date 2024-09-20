using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    /// <summary>
    /// 血液净化次数
    /// </summary>
    public class BloodPurificationNumDto
    {
        /// <summary>
        /// 血液净化次数
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        
        /// <summary>
        /// 总时长
        /// </summary>
        public decimal Total { get; set; }
    }
}
