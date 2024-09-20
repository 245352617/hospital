using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 诊断记录查询Dto
    /// </summary>
    public class DiagnoseRecordInput
    {
        /// <summary>
        /// 自增Id
        /// </summary>
        public int PD_ID { get; set; }

        /// <summary>
        /// 分诊患者Id（PVID）
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        public string DoctorCode { get; set; }

        /// <summary>
        /// 诊断分类  开立 = 1，收藏 = 2 默认开立
        /// </summary>
        public DiagnoseClass DiagnoseClassCode { get; set; } = DiagnoseClass.开立;


        /// <summary>
        /// 医生建议
        /// </summary>
        public string DoctorAdvice { get; set; }
    }
}