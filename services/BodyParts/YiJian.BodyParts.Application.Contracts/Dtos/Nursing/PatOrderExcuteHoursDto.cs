using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class PatOrderExcuteHoursDto
    {
        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 入院id
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人id
        /// </summary>
        public string ArchiveId { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 病人姓名
        /// </summary>
        public string PatName { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public DateTime InDeptTime { get; set; }

        /// <summary>
        /// 合计小时数
        /// </summary>
        public int TotalHours { get; set; }

        public Dictionary<string,string> DateList { get; set; }
    }
}
