using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 诊疗项
    /// </summary>
    [Comment("诊疗项")]
    public class Treat : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 医嘱ID
        /// </summary>
        [Comment("医嘱ID")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 其它价格
        /// </summary>
        [Comment("其它价格")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal? OtherPrice { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [Comment("规格")]
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 默认频次代码
        /// </summary>
        [Comment("默认频次代码")]
        [StringLength(50)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary>
        [Comment("收费大类代码")]
        [StringLength(50)]
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary>
        [Comment("收费小类代码")]
        [StringLength(50)]

        public string FeeTypeSubCode { get; set; }

        /// <summary>
        /// 项目类型
        /// </summary>
        [Comment("项目类型")]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目类型名称
        /// </summary>
        [Comment("项目类型名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 附加类型 0=否 1=用法附加处置 2=皮试附加处置
        /// </summary>
        [Comment("附加类型")]
        public int AdditionalItemsType { get; set; }

        /// <summary>
        /// 处置关联处方医嘱ID
        /// </summary>
        [Comment("处置关联处方医嘱ID")]
        public Guid? AdditionalItemsId { get; set; }
    }
}
