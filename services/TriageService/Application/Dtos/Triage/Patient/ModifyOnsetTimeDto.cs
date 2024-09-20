using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 修改发病时间Dto
    /// </summary>
    public class ModifyOnsetTimeDto
    {
        /// <summary>
        /// 发病时间
        /// </summary>
        public DateTime OnsetTime { get; set; }

        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid TriagePatientInfoId { get; set; }
    }
}