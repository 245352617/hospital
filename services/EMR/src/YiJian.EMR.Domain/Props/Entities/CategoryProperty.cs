using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.Props.Entities
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    [Comment("电子病历属性")]
    public class CategoryProperty : Entity<Guid>
    {
        /// <summary>
        /// 电子病历属性
        /// </summary>
        private CategoryProperty()
        {
        }

        /// <summary>
        /// 电子病历属性
        /// </summary>
        public CategoryProperty(
            Guid id,
            [NotNull] string value,
            [NotNull] string label,
            int lv = 0,
            Guid? parentId = null)
        {
            Id = id;
            Value = value;
            Label = label;
            Lv = lv;
            ParentId = parentId;
        }

        /// <summary>
        /// 属性值
        /// </summary>
        [Comment("属性值")]
        [StringLength(50)]
        public string Value { get; set; }

        /// <summary>
        /// 属性标签
        /// </summary>
        [Comment("属性标签")]
        [StringLength(32)]
        public string Label { get; set; }

        /// <summary>
        /// 属性层级 level（一级默认=0）
        /// </summary>
        [Comment("属性层级")]
        public int Lv { get; set; } = 0;

        /// <summary>
        /// 级联父节点Id(空属于一级)
        /// </summary>
        [Comment("级联父节点Id")]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 更新
        /// </summary> 
        public void Update(
            [NotNull] string label)
        {
            Label = label;
        }

    }

}
