using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 添加诊断
    /// </summary>
    public class CreateDiagDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 诊断类别
        /// </summary>
        /// <example></example>
        public string DiagType { get; set; }

        /// <summary>
        /// 诊断内容
        /// </summary>
        /// <example></example>
        [Required]
        public string Diagnosis { get; set; }

        /// <summary>
        /// 诊断ICD10代码
        /// </summary>
        /// <example></example>
        public string Icd10 { get; set; }

        /// <summary>
        /// 自定义原因
        /// </summary>
        /// <example></example>
        public string Reason { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
    }
}
