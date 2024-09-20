using System;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 描    述:检查病理小项Dto
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/24 14:58:35
    /// </summary>
    public class PacsPathologyItemDto
    {
        /// <summary>
        /// 检查项Id
        /// </summary>
        public Guid PacsId { get; set; }

        /// <summary>
        /// 标本名称多个用","隔开
        /// </summary>
        public string Specimen { get; set; }

        /// <summary>
        /// 取材部位
        /// </summary>
        public string DrawMaterialsPart { get; set; }

        /// <summary>
        /// 标本数量
        /// </summary>
        public int SpecimenQty { get; set; }

        /// <summary>
        /// 离体时间
        /// </summary>
        public DateTime LeaveTime { get; set; }

        /// <summary>
        /// 固定时间
        /// </summary>
        public DateTime RegularTime { get; set; }

        /// <summary>
        /// 特异性感染
        /// </summary>
        public string SpecificityInfect { get; set; }

        /// <summary>
        /// 申请目的
        /// </summary>
        public string ApplyForObjective { get; set; }

        /// <summary>
        /// 临床症状及体征
        /// </summary>
        public string Symptom { get; set; }
    }
}
