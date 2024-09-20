using System;
using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者分诊信息队列Dto
    /// </summary>
    public class PatientInfoMqDto
    {
        /// <summary>
        /// 院前分诊患者基本信息
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        /// <summary>
        /// 院前分诊生命体征信息
        /// </summary>
        public VitalSignInfoDto VitalSignInfo { get; set; }

        /// <summary>
        /// 院前分诊结果信息
        /// </summary>
        public ConsequenceInfoDto ConsequenceInfo { get; set; }

        /// <summary>
        /// 院前分诊评分信息
        /// </summary>
        public ICollection<ScoreInfoDto> ScoreInfo { get; set; }

        /// <summary>
        /// 入院情况
        /// </summary>
        public AdmissionInfoDto AdmissionInfo { get; set; }

        /// <summary>
        /// 挂号信息
        /// </summary>
        public RegisterInfoDto RegisterInfo { get; set; }

        /// <summary>
        /// 群伤
        /// </summary>
        public GroupInjuryInfoDto GroupInjuryInfo { get; set; }

        /// <summary>
        /// 告知单患者
        /// </summary>
        public InformPatDto Inform { get; set; }

    }
}