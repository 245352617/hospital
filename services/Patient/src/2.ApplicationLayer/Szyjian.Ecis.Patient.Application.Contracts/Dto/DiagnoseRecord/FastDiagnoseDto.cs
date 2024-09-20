using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 快速诊断Dto
    /// </summary>
    public class FastDiagnoseDto
    {
        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        public string PyCode { get; set; }
        /// <summary>
        /// 医学类型
        /// </summary>
        public MedicalTypeEnum MedicalType { get; set; }
        /// <summary>
        /// Icd10
        /// </summary>

        public string Icd10 { get; set; }
    }

}


