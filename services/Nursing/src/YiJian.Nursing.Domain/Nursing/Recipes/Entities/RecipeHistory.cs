using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 医嘱操作历史
    /// </summary>
    [Comment("医嘱操作历史")]
    public class RecipeHistory : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Comment("医嘱ID")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Comment("操作类型")]
        public EDoctorsAdviceOperation Operation { get; set; }

        /// <summary>
        /// 操作人编码
        /// </summary>
        [Comment("操作人编码")]
        [StringLength(30)]
        public string OperatorCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary>
        [Comment("操作人名称")]
        [StringLength(20)]
        public string OperatorName { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        [Comment("操作时间")]
        public DateTime OperaTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [StringLength(1000)]
        public string Remark { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        protected RecipeHistory() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public RecipeHistory(Guid id) : base(id) { }
    }
}
