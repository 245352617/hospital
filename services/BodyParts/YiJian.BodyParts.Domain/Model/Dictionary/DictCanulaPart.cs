using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:人体图-编号字典
    /// </summary>
    public class DictCanulaPart : Entity<Guid>
    {
        public DictCanulaPart() { }

        public DictCanulaPart(Guid id) : base(id) { }


        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        [StringLength(20)]
        [Required]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 部位名称
        /// </summary>
        [StringLength(80)]
        [Required]
        public string PartName { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string PartNumber { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        public int SortNum { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        [Required]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 风险级别 默认空，1低危 2中危 3高危
        /// 皮肤分期 默认空  1-1期 2-2期 3-3期 4-4期 5-深部组织损伤 6-不可分期
        /// </summary>
        public string RiskLevel { get; set; }
    }
}
