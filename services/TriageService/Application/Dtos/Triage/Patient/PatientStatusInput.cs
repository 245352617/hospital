using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 查询患者状态输入项
    /// </summary>
    public class PatientStatusInput
    {
        /// <summary>
        /// 任务单Id
        /// </summary>
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 多个任务单Id拼接字符串
        /// </summary>
        public string TaskInfoIds { get; set; }

        /// <summary>
        /// 救护车车牌号
        /// </summary>
        public string CarNum { get; set; }
        
        /// <summary>
        /// 患者病历号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 分诊患者基本信息Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }

        /// <summary>
        /// 身份证号码
        /// </summary>
        public string IdentityNos { get; set; }
    }
}