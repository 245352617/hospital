using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:皮肤主表
    /// </summary>
    public class IcuNursingSkin : Entity<Guid>
    {
        public IcuNursingSkin() { }

        public IcuNursingSkin(Guid id) : base(id) { }

        /// <summary>
        /// 发生时间
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(50)]
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 压疮部位
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressPart { get; set; }

        /// <summary>
        /// 压疮类型编码
        /// </summary>
        /// <example></example>
        [CanBeNull]
        [StringLength(50)]
        public string ModuleCode { get; set; }

        /// <summary>
        /// 压疮类型
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressType { get; set; }

        /// <summary>
        /// 压疮来源
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressSource { get; set; }

        /// <summary>
        /// 压疮分期
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        public string PressStage { get; set; }

        /// <summary>
        /// 压疮面积
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressArea { get; set; }

        /// <summary>
        /// 伤口颜色
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressColor { get; set; }

        /// <summary>
        /// 伤口气味
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string PressSmell { get; set; }

        /// <summary>
        /// 渗出液颜色
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string ExudateColor { get; set; }

        /// <summary>
        /// 渗出液量
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string ExudateAmount { get; set; }

        /// <summary>
        /// 护士Id
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        [CanBeNull]
        [StringLength(50)]
        public string NurseName { get; set; }

        /// <summary>
        /// 是否结束
        /// </summary>
        public bool Finished { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? FinishTime { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        [Required]
        public int ValidState { get; set; }

        /// <summary>
        /// 皮肤记录
        /// </summary>
        [CanBeNull]
        public string CanulaRecord { get; set; }
    }
}
