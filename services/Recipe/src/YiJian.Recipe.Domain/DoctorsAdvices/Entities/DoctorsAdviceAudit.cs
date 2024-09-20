using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;
using YiJian.DoctorsAdvices.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 医嘱审计
    /// </summary>
    [Comment("医嘱审计")]
    public class DoctorsAdviceAudit : Entity<Guid>
    {
        /// <summary>
        /// 医嘱审计
        /// </summary>
        private DoctorsAdviceAudit()
        {

        }

        /// <summary>
        /// 医嘱审计
        /// </summary> 
        public DoctorsAdviceAudit(Guid id,
            ERecipeStatus status,
            string operationCode,
            string operationName,
            DateTime operationTime,
            EOriginType originType,
            Guid doctorsAdviceId)
        {
            Id = id;
            Status = status;
            OperationCode = operationCode;
            OperationName = operationName;
            OperationTime = operationTime;
            OriginType = originType;
            DoctorsAdviceId = doctorsAdviceId;
        }

        /// <summary>
        /// 状态
        /// </summary>
        [Comment("状态")]
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 操作人编码（驳回操作人，复核操作人，执行操作人）
        /// </summary> 
        [Comment("操作人编码（驳回操作人，复核操作人，执行操作人）")]
        [StringLength(20)]
        public string OperationCode { get; set; }

        /// <summary>
        /// 操作人名称
        /// </summary> 
        [Comment("操作人名称")]
        [StringLength(50)]
        public string OperationName { get; set; }

        /// <summary>
        /// 操作时间（驳回时间，复核时间，执行时间）
        /// </summary>
        [Comment("操作时间（驳回时间，复核时间，执行时间）")]
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 操作来源，0=医生操作，1=护士操作
        /// </summary>
        [Comment("操作来源，0=医生操作，1=护士操作")]
        public EOriginType OriginType { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        public DoctorsAdvice DoctorsAdvice { get; set; }

    }
}
