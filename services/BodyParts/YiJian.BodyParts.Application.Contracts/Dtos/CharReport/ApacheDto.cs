using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 当月患者APACHEII
    /// </summary>
    public class ApacheDto
    {
        /// <summary>
        /// 每月起始时间
        /// </summary>
        public DateTime startMonth { get; set; }

        /// <summary>
        /// 总评估率
        /// </summary>
        public decimal assessmentrate { get; set; }

        /// <summary>
        /// 未评估
        /// </summary>
        public decimal noassess { get; set; }

        /// <summary>
        /// ＜10分
        /// </summary>
        public decimal ten { get; set; }

        /// <summary>
        /// 10-15分
        /// </summary>
        public decimal fifteen { get; set; }

        /// <summary>
        /// 15-20分
        /// </summary>
        public decimal twenty { get; set; }

        /// <summary>
        /// 20-25分
        /// </summary>
        public decimal twentyfive { get; set; }

        /// <summary>
        /// ＞25分
        /// </summary>
        public decimal fiveabove { get; set; }
    }
}
