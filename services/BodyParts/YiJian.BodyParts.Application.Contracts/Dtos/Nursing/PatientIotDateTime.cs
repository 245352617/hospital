using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class PatientIotDateTime
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        public DateTime NurseTime { get; set; }
    }
}
