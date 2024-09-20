using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class TriagePatientInfosMqDto
    {
        /// <summary>
        /// 院前分诊患者基本信息
        /// </summary>
        public PatientInfoMqDto PatientInfo { get; set; }

        /// <summary>
        /// 院前分诊生命体征信息
        /// </summary>
        public VitalSignInfoMqDto VitalSignInfo { get; set; }

        /// <summary>
        /// 院前分诊结果信息
        /// </summary>
        public ConsequenceInfoMqDto ConsequenceInfo { get; set; }

        /// <summary>
        /// 院前分诊评分信息
        /// </summary>
        public ICollection<ScoreInfoMqDto> ScoreInfo { get; set; }

        /// <summary>
        /// 入院情况
        /// </summary>
        public AdmissionInfoMqDto AdmissionInfo { get; set; }

        /// <summary>
        /// 挂号信息
        /// </summary>
        public RegisterInfoMqDto RegisterInfo { get; set; }
    }
}