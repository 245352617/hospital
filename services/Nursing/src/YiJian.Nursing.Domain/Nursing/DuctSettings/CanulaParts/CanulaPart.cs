using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing
{
    /// <summary>
    /// 表:人体图-编号字典
    /// </summary>
    [Comment("表:人体图-编号字典")]
    public class CanulaPart : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public CanulaPart()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public CanulaPart(Guid id) : base(id)
        {
        }


        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [Comment("科室代码")]
        public string DeptCode { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        [StringLength(20)]
        [Required]
        [Comment("模块代码")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 部位名称
        /// </summary>
        [StringLength(80)]
        [Required]
        [Comment("部位名称")]
        public string PartName { get; set; }

        /// <summary>
        /// 部位编号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("部位编号")]
        public string PartNumber { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        [Required]
        [Comment("排序")]
        public int Sort { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        [Comment("是否可用")]
        public bool IsEnable { get; set; }
    }
}
