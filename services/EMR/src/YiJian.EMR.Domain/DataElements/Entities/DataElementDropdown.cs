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
    /// 输入域类型下拉项目
    /// </summary>
    [Comment("输入域类型下拉项目")]
    public class DataElementDropdown : FullAuditedAggregateRoot<Guid>
    {

        /// <summary>
        /// 元素项Id
        /// </summary>
        [Comment("元素项Id")]
        public Guid DataElementItemId { get;set;}

        /// <summary>
        /// 允许多选
        /// </summary>
        [Comment("允许多选")]
        public bool IsAllowMultiple { get;set; }

        /// <summary>
        /// 数值勾选按照时间排序
        /// </summary>
        [Comment("数值勾选按照时间排序")]
        public bool IsSortByTime { get; set; }

        /// <summary>
        /// 动态加载下拉列表
        /// </summary>
        [Comment("动态加载下拉列表")]
        public bool IsDynamicallyLoad { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        [Comment("来源")]
        [StringLength(50)]
        public string DataSource { get; set; }

    }
}
