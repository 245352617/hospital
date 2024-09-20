using System.Collections.Generic;

namespace YiJian.Documents.Dto
{

    /// <summary>
    /// 处方单信息
    /// </summary>
    public class AdviceDocumentDto
    {
        /// <summary>
        /// 药方医嘱
        /// </summary>
        public List<MedicineAdviceDto> Medicines { get; set; } = new List<MedicineAdviceDto>();

        /// <summary>
        /// 检验医嘱
        /// </summary>
        public List<LisAdviceDto> Lises { get; set; } = new List<LisAdviceDto>();

        /// <summary>
        /// 检查医嘱
        /// </summary>
        public List<PacsAdviceDto> Pacses { get; set; } = new List<PacsAdviceDto>();

        /// <summary>
        /// 诊疗
        /// </summary>
        public List<TreatAdviceDto> Treats { get; set; } = new List<TreatAdviceDto>();

    }
}
