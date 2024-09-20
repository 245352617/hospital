using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.EMR.DataElements.Entities
{
    /// <summary>
    /// 元素下拉项
    /// </summary>
    [Comment("元素下拉项")]
    public class DataElementDropdownItem : Entity<Guid>
    {
        /// <summary>
        /// 输入域类型下拉项目Id
        /// </summary>
        [Comment("输入域类型下拉项目Id")]
        public Guid DataElementDropdownId { get; set; }

        /// <summary>
        /// 文本
        /// </summary>
        [Comment("文本")]
        [StringLength(50)]
        public string Text { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        [Comment("数值")]
        [StringLength(50)]
        public string Value { get; set; }

        /// <summary>
        /// 指定的列表文本
        /// </summary>
        [Comment("指定的列表文本")]
        [StringLength(50)]
        public string ListText { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Comment("分组名称")]
        [StringLength(50)]
        public string GroupName { get; set; }

    }
}
