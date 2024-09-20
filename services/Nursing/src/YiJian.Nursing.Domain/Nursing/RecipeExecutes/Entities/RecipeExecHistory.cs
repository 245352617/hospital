using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Nursing.RecipeExecutes.Entities
{
    /// <summary>
    /// 医嘱执行历史记录
    /// </summary>
    [Comment("医嘱执行历史记录")]
    public class RecipeExecHistory : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 计划执行时间（计划执行时间）
        /// </summary>
        [Comment("计划执行时间")]
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 实际操作时间
        /// </summary>
        [Comment("实际操作时间")]
        public DateTime? OperationTime { get; set; }

        /// <summary>
        /// 护士编码
        /// </summary>
        [Comment("护士编码")]
        [StringLength(30)]
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        [Comment("护士名称")]
        [StringLength(20)]
        public string NurseName { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [Comment("操作类型")]
        public EDoctorsAdviceOperation OperationType { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 医嘱执行Id
        /// </summary>
        [Comment("医嘱执行Id")]
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 操作内容
        /// </summary>
        [Comment("操作内容")]
        public string OperationContent { get; set; }
    }
}
