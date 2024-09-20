using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 为患者关联群伤事件Dto
    /// </summary>
    public class AddGroupInjuryForPatientDto
    {
        /// <summary>
        /// 分诊患者Id字符串，使用 ‘,’ 拼接
        /// </summary>
        public string TriagePatientInfoIds { get; set; }

        /// <summary>
        /// 群伤事件Id
        /// </summary>
        public Guid GroupInjuryId { get; set; }
    }
}