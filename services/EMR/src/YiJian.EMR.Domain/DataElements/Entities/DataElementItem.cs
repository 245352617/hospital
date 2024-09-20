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
    /// 数据元素
    /// </summary>
    [Comment("数据元素")]
    public class DataElementItem : FullAuditedAggregateRoot<Guid>
    {  
        /// <summary>
        /// 编号
        /// </summary>
        [Comment("编号")]
        [StringLength(32)]
        public string No { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Comment("名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 背景文本
        /// </summary>
        [Comment("背景文本")]
        [StringLength(50)]
        public string Watermark { get; set; }

        /// <summary>
        /// 提示文本
        /// </summary>
        [Comment("提示文本")]
        [StringLength(50)]
        public string Tips { get; set; }

        /// <summary>
        /// 起始边框
        /// </summary>
        [Comment("起始边框")]
        [StringLength(50)]
        public string BeginMargin { get; set; }

        /// <summary>
        /// 结尾边框
        /// </summary>
        [Comment("结尾边框")]
        [StringLength(50)]
        public string EndMargin { get; set; }

        /// <summary>
        /// 固定宽度
        /// </summary>
        [Comment("固定宽度")]
        public int FixedWidth { get; set; }

        /// <summary>
        /// 只读状态
        /// </summary>
        [Comment("只读状态")]
        public bool IsReadOnly { get; set; }

        /// <summary>
        /// 数据源
        /// </summary>
        [Comment("数据源")]
        [StringLength(50)]
        public string DataSource { get; set; }

        /// <summary>
        /// 绑定路径
        /// </summary>
        [Comment("绑定路径")]
        [StringLength(50)]
        public string BindPath { get; set; }

        /// <summary>
        /// 级联表达式
        /// </summary>
        [Comment("级联表达式")]
        [StringLength(100)]
        public string CascadeExpression { get; set; }

        /// <summary>
        /// 数值表达式
        /// </summary>
        [Comment("数值表达式")]
        [StringLength(100)]
        public string NumericalExpression { get; set; }

        /// <summary>
        /// 用户可以直接修改内容
        /// </summary>
        [Comment("用户可以直接修改内容")] 
        public bool CanModify { get; set; }

        /// <summary>
        /// 允许被删除
        /// </summary>
        [Comment("允许被删除")]
        public bool CanDelete { get; set; }

        /// <summary>
        /// 输入域类型
        /// </summary>
        [Comment("输入域类型")]
        [StringLength(50)]
        public string InputType { get; set; }

        /// <summary>
        ///  是否开启校验
        /// </summary>
        [Comment("是否开启校验")]
        public bool NeedVerification { get; set; }

        /// <summary>
        ///  校验表达式
        /// </summary>
        [Comment("校验表达式")]
        [StringLength(200)]
        public string VerificationExpression { get; set; }

        /// <summary>
        /// 数据元Id
        /// </summary>
        [Comment("数据元Id")]
        public Guid DataElementId { get;set;}

    }
}
