using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:病人提醒
    /// </summary>
    public class IcuPatientRemindDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        /// <example></example>
        [Required]
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ParaName { get; set; }

        /// <summary>
        /// 单位名称
        /// </summary>
        [Required]
        public string UnitName { get; set; }

        /// <summary>
        /// 高值
        /// </summary>
        /// <example></example>
        public string HightValue { get; set; }

        /// <summary>
        /// 低值
        /// </summary>
        /// <example></example>
        public string LowValue { get; set; }
    }
}
