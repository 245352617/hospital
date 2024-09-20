using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 历史诊断Dto
    /// </summary>
    public class HistoryDiagnoseDto
    {
        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 诊断编码
        /// </summary>
        public string DiagnoseCode { get; set; }

        /// <summary>
        /// 诊断内容
        /// </summary>
        public string DiagnoseName { get; set; }

        /// <summary>
        /// 就诊日期
        /// </summary>
        public DateTime VisitDate { get; set; }

        /// <summary>
        /// 开立医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 诊断说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 诊断类型名称
        /// </summary>
        public string DiagnoseTypeName { get; set; }

        /// <summary>
        /// 开立时间
        /// </summary>
        public string OpenTime { get; set; }
        /// <summary>
        /// icd10
        /// </summary>
        public string Icd10 { get; set; }
        /// <summary>
        /// 就诊流水号
        /// </summary>
        public string VisitSerialNo { get; set; }
        /// <summary>
        /// 医学类型
        /// </summary>
        public MedicalTypeEnum MedicalType { get; set; }
    }
}