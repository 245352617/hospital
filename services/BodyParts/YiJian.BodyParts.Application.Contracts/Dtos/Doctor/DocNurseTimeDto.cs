using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 护理监测时间列表
    /// </summary>
    public class DocNurseTimeDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }
    }
}
