using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing
{
    /// <summary>
    /// 导管护理信息表
    /// （关联病人导管基础信息表，具体的病人导管护理信息）
    /// </summary>
    [Comment("导管护理信息表")]
    public class Canula : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public Canula() { }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public Canula(Guid id) : base(id) { }

        /// <summary>
        /// FK 关联NursingCanula表Id
        /// </summary>
        [Comment("FK 关联NursingCanula表Id")]
        public Guid CanulaId { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        [Required]
        [Comment("护理时间")]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 置入方式
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        [Comment("置入方式")]
        public string CanulaWay { get; set; }

        /// <summary>
        /// 置管长度
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("置管长度")]
        public string CanulaLength { get; set; }

        /// <summary>
        /// 护士Id
        /// </summary>
        [CanBeNull]
        [StringLength(10)]
        [Comment("护士Id")]
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("护士名称")]
        public string NurseName { get; set; }

        /// <summary>
        /// 导管操作记录
        /// </summary>
        [Comment("导管操作记录")]
        public string CanulaRecord { get; set; }
    }
}
