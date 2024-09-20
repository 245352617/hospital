using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-参数字典
    /// </summary>
    public class CreateUpdateDictDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        [Required]
        public string ParaName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        /// <example></example>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        /// <example></example>
        [Required]
        public string DictValue { get; set; }

        /// <summary>
        /// 字典值说明
        /// </summary>
        public string DictDesc { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 是否默认（1-是，0-否）
        /// </summary>
        /// <example></example>
        public bool IsDefault { get; set; }
    }
}
