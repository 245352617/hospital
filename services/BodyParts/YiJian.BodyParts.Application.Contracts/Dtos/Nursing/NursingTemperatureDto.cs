using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    /// <summary>
    /// 体温单数据
    /// </summary>
    public class NursingTemperatureDto
    {
        /// <summary>
        /// 项目编码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 项目值
        /// </summary>
        public string ParaValue { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 护理人编码
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护理人姓名
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }
    }
}
