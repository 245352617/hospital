using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-诊断
    /// </summary>
    public class DictDiagDto : EntityDto<Guid>
    {
        /// <summary>
        /// 诊断类别
        /// </summary>
        /// <example></example>
        public string DiagType { get; set; }

        /// <summary>
        /// 诊断ICD10代码
        /// </summary>
        /// <example></example>
        public string Icd10 { get; set; }

        /// <summary>
        /// 诊断内容
        /// </summary>
        /// <example></example>
        public string DiagText { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        /// <example></example>
        public string PinYin { get; set; }

        /// <summary>
        /// 有效状态
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
