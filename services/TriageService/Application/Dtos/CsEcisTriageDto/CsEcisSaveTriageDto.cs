using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// CS版急诊保存分诊Dto
    /// </summary>
    public class CsEcisSaveTriageDto
    {
        /// <summary>
        /// 分诊患者信息
        /// </summary>
        public List<PatientVisitDto> PvList { get; set; }

        /// <summary>
        /// 群伤事件
        /// </summary>
        public CsEcisGroupInjuryDto GroupInjury { get; set; }
    }
}