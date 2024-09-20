using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Patient
{
    public class PatThdDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 特护单护理日期集合，只有交班的才显示出来
        /// </summary>
        public List<DateTime> NursinDateList { get; set; }
    }
}
