using System.ComponentModel.DataAnnotations;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 添加收藏诊断Dto
    /// </summary>
    public class CreateCollectionDiagnoseDto
    {
        /// <summary>
        /// 诊断编码
        /// </summary>
        [MaxLength(50)]
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断名称
        /// </summary>
        [MaxLength(100)]
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [MaxLength(100)]
        public string PyCode { get; set; }

        /// <summary>
        /// 医学类型
        /// </summary>
        public MedicalTypeEnum MedicalType { get; set; }
        /// <summary>
        /// Icd10
        /// </summary>
        [StringLength(20)]

        public string Icd10 { get; set; }
    }
}