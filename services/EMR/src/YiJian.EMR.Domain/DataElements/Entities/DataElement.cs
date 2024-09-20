using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore; 
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.DataElements.Entities
{
    /// <summary>
    /// 数据元集合根
    /// </summary>
    [Comment("数据元集合根")]
    public class DataElement : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 名称标题
        /// </summary>
        [Comment("名称标题")]
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 是否是元素
        /// </summary>
        [Comment("是否是元素")]
        public bool IsElement { get; set; }

        /// <summary>
        /// 父级级联Id
        /// </summary>
        [Comment("父级级联Id")]
        public Guid Parent { get; set; }

        /// <summary>
        /// 层级，默认=0
        /// </summary>
        [Comment("层级，默认=0")]
        public int Lv { get; set; }

    }

}
