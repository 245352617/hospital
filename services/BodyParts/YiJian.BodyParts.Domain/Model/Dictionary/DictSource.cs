using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:字典-基础字典设置表
    /// </summary>
    public class DictSource : Entity<Guid>
    {
        public DictSource() { }

        public DictSource(Guid id) : base(id) { }

        /// <summary>
        /// 参数类型(S-系统参数，D-科室参数)
        /// </summary>
        [StringLength(10)]
        [Required]
        public string ParaType { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string DeptCode { get; set; }

        /// <summary>
        /// 参数所属模块
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 模板名称
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string ModuleName { get; set; }

        /// <summary>
        /// 父级Id
        /// </summary>
        public Guid? Pid { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [StringLength(50)]
        [CanBeNull]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string ParaName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public bool ParaValue { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
