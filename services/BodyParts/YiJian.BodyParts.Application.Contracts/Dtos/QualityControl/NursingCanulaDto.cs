using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class NursingCanulaByPatientDto
    {
        /// <summary>
        /// 导管类型
        /// </summary>
        public string CanulaType { get; set; }

        /// <summary>
        /// 患者列表
        /// </summary>
        public List<PatientCanlaDto> patientCanlaDtos { get; set; }
    }
    public class PatientCanlaDto
    {
        /// <summary>
        /// 患者导管信息列表
        /// </summary>
        public List<dynamic> PatientCanulaInfos { get; set; }
    }
}
