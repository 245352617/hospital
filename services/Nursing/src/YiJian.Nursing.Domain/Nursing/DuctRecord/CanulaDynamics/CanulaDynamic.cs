using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing
{
    /// <summary>
    /// 导管附加动态参数表
    /// （关联病人导管基础信息表，具体的病人导管）
    /// </summary>
    [Comment("导管附加动态参数表")]
    public class CanulaDynamic : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public CanulaDynamic()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public CanulaDynamic(Guid id) : base(id)
        {
        }

        /// <summary>
        /// FK 关联NursingCanula表Id
        /// </summary>
        [Required]
        [Comment("FK 关联NursingCanula表Id")]
        public Guid CanulaId { get; set; }

        /// <summary>
        /// 管道分类
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("管道分类")]
        public string GroupName { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("参数代码")]
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        [Comment("参数名称")]
        public string ParaName { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        [Comment("参数值")]
        public string ParaValue { get; set; }
    }
}