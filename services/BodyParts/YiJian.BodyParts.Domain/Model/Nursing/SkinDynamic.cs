using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表：人体图动态表
    /// </summary>
    public class SkinDynamic : Entity<Guid>
    {
        public SkinDynamic() { }

        public SkinDynamic(Guid id) : base(id) { }

        /// <summary>
        /// 主键
        /// </summary>
        [Required]
        public Guid CanulaId { get; set; }

        /// <summary>
        /// 皮肤分类
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        public string GroupName { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        public string ParaName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        public string ParaValue { get; set; }
    }
}
