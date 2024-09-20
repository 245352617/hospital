using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 皮肤护理信息
    /// </summary>
    public class NursingSkinDto
    {
        /// <summary>
        /// 皮肤记录Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 压疮部位
        /// </summary>
        /// <example></example>
        public string PressPart { get; set; }

        /// <summary>
        /// 发生时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 持续天数
        /// </summary>
        /// <example></example>
        public int Days { get; set; }

        /// <summary>
        /// 压疮类型编码
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 压疮类型
        /// </summary>
        /// <example></example>
        public string PressType { get; set; }

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
        /// 护士Id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
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
        /// 人体图部位编号
        /// </summary>
        /// <example></example>
        public string PartNumber { get; set; }

        /// <summary>
        /// 皮肤记录
        /// </summary>
        public string CanulaRecord { get; set; } = string.Empty;

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;

        /// <summary>
        /// 动态列表
        /// </summary>
        public List<CanulaItemDto> CanulaItemDto { get; set; }
    }
}
