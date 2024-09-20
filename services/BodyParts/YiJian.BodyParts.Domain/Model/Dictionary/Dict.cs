using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:字典-通用业务
    /// </summary>
    public class Dict : Entity<Guid>
    {
        public Dict() { }

        public Dict(Guid id) : base(id) { }


        /// <summary>
        /// 参数代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [StringLength(40)]
        [Required]
        public string ParaName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        [StringLength(80)]
        [Required]
        public string DictValue { get; set; }

        /// <summary>
        /// 字典值说明
        /// </summary>
        [StringLength(200)]
        public string DictDesc { get; set; }


        /// <summary>
        /// 上级代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string ParentId { get; set; }

        /// <summary>
        /// 字典标准（国标、自定义）
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string DictStandard { get; set; }

        /// <summary>
        /// HIS对照代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string HisCode { get; set; }

        /// <summary>
        /// HIS对照
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        public string HisName { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int SortNum { get; set; }

        /// <summary>
        /// 是否默认
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        [Required]
        public int ValidState { get; set; }

    }
}
