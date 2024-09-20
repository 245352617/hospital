using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:字典-药品用法
    /// </summary>
    public class CreateUpdateDictUsageDto : EntityDto<Guid>
    {
        /// <summary>
        /// 用法代码
        /// </summary>
        /// <example></example>
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        /// <example></example>
        [Required]
        public string UsageName { get; set; }

        /// <summary>
        /// 用法全称
        /// </summary>
        /// <example></example>
        [Required]
        public string UsageFullName { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 所属分组
        /// </summary>
        /// <example></example>
        public string NursingType { get; set; }

        /// <summary>
        /// 是否单次
        /// </summary>
        public bool? Single { get; set; }

        /// <summary>
        /// 是否需要提取
        /// </summary>
        public bool Extract { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? SortNum { get; set; }
    }
}
