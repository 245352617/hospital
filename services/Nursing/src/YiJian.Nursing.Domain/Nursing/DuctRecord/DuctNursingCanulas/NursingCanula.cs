using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Nursing
{
    /// <summary>
    /// 病人导管基础信息表
    /// </summary>
    [Comment("病人导管基础信息表")]
    public class NursingCanula : FullAuditedEntity<Guid>
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        public NursingCanula()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public NursingCanula(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 患者id
        /// </summary>
        [Comment("患者id")]
        public Guid PI_ID { get; set; }

        // /// <summary>
        // /// 住院流水号
        // /// </summary>
        // [StringLength(20)]
        // [Required]
        // public string InHosNum { get; set; }

        /// <summary>
        /// 插管时间
        /// </summary>
        [Comment("插管时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 拔管时间
        /// </summary>
        [Comment("拔管时间")]
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        [Comment("导管分类")]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 导管名称
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(20)]
        [Comment("导管名称")]
        public string ModuleName { get; set; }

        /// <summary>
        /// 管道名称
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("管道名称")]
        public string CanulaName { get; set; }

        /// <summary>
        /// 插管部位
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("插管部位")]
        public string CanulaPart { get; set; }

        /// <summary>
        /// 插管次数
        /// </summary>
        [Comment("插管次数")]
        public int? CanulaNumber { get; set; }

        /// <summary>
        /// 插管地点
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        [Comment("插管地点")]
        public string CanulaPosition { get; set; }

        /// <summary>
        /// 置管人代码
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        [Comment("置管人代码")]
        public string DoctorId { get; set; }

        /// <summary>
        /// 置管人名称
        /// </summary>
        [CanBeNull]
        [StringLength(100)]
        [Comment("置管人名称")]
        public string DoctorName { get; set; }

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
        /// 拔管原因
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        [Comment("拔管原因")]
        public string DrawReason { get; set; }

        /// <summary>
        /// 管道状态
        /// </summary>
        [Comment("管道状态")]
        public TubeDrawStateEnum TubeDrawState { get; set; }

        /// <summary>
        /// 使用标志：（Y在用，N已拔管）
        /// </summary>
        [Required]
        [StringLength(4)]
        [Comment("使用标志：（Y在用，N已拔管）")]
        public string UseFlag { get; set; }

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
        /// 护理时间
        /// </summary>
        [Comment("护理时间")]
        public DateTime? NurseTime { get; set; }

        /// <summary>
        /// 导管操作记录
        /// </summary>
        [Comment("导管操作记录")]
        public string CanulaRecord { get; set; }
    }
}