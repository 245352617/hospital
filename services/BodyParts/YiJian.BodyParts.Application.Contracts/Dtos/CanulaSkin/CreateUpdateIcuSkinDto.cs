using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:皮肤详细信息记录表
    /// </summary>
    public class CreateUpdateIcuSkinDto : EntityDto<Guid>
    {
        /// <summary>
        /// 压疮Id
        /// </summary>
        /// <example></example>
        public Guid? SkinId { get; set; }

        /// <summary>
        /// 压疮分期
        /// </summary>
        /// <example></example>
        public string PressStage { get; set; }

        /// <summary>
        /// 压疮面积
        /// </summary>
        public string PressArea { get; set; }

        /// <summary>
        /// 伤口颜色
        /// </summary>
        /// <example></example>
        public string PressColor { get; set; }

        /// <summary>
        /// 伤口气味
        /// </summary>
        /// <example></example>
        public string PressSmell { get; set; }

        /// <summary>
        /// 渗出液颜色
        /// </summary>
        /// <example></example>
        public string ExudateColor { get; set; }

        /// <summary>
        /// 渗出液量
        /// </summary>
        /// <example></example>
        public string ExudateAmount { get; set; }

        /// <summary>
        /// 护理措施
        /// </summary>
        /// <example></example>
        public string NursingMeasure { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 护士Id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 皮肤护理记录
        /// </summary>
        public string CanulaRecord { get; set; }
    }
}
