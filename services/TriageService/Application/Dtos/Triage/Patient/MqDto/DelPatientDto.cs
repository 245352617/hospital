using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 删除患者Dto
    /// </summary>
    public class DelPatientDto
    {
        /// <summary>
        /// 分诊患者基本信息Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }
        
        /// <summary>
        /// 是否删除分诊记录
        /// </summary>
        public bool IsDelTriageRecord { get; set; } = true;
    }
}