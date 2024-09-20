using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 呼吸监测护理记录
    /// </summary>
    public class DocNursingDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParaValue { get; set; }
    }
}
