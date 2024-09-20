using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 预警提醒
    /// </summary>
    public class ParaRemindDto
    {
        /// <summary>
        /// 床位号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParaValue { get; set; }

        /// <summary>
        /// 预警说明
        /// </summary>
        public string ParaExplain { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}
