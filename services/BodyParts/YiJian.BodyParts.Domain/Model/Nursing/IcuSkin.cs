using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:皮肤详细信息记录表
    /// </summary>
    public class IcuSkin : Entity<Guid>
    {
        public IcuSkin() { }

        public IcuSkin(Guid id) : base(id) { }


        /// <summary>
        /// 压疮Id
        /// </summary>

        public Guid? SkinId { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

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
        /// 护理措施
        /// </summary>
        [CanBeNull]
        [StringLength(255)]
        public string NursingMeasure { get; set; }

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
        /// 皮肤护理记录
        /// </summary>
        [CanBeNull]
        public string CanulaRecord { get; set; }
    }
}
