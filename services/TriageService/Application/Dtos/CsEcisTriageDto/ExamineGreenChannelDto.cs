using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊审核绿通Dto
    /// </summary>
    public class ExamineGreenChannelDto
    {
        /// <summary>
        /// PVID
        /// </summary>
        public Guid PVID { get; set; }

        /// <summary>
        /// 病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 就诊次数
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 审核医生
        /// </summary>
        public string ExamineDoctor { get; set; }

        /// <summary>
        /// 审核时间
        /// </summary>
        public DateTime ExamineDT { get; set; }
    }
}