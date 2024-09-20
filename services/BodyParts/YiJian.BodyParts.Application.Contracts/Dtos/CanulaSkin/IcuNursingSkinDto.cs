using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:皮肤主表
    /// </summary>
    public class IcuNursingSkinDto : EntityDto<Guid>
    {
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
        /// 持续天数
        /// </summary>
        /// <example></example>
        public int Days { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 压疮部位
        /// </summary>
        /// <example></example>
        public string PressPart { get; set; }

        /// <summary>
        /// 压疮类型
        /// </summary>
        /// <example></example>
        public string PressType { get; set; }

        /// <summary>
        /// 压疮来源
        /// </summary>
        /// <example></example>
        public string PressSource { get; set; }

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
        /// 人体图部位编号
        /// </summary>
        /// <example></example>
        public string PartNumber { get; set; }
    }
}
