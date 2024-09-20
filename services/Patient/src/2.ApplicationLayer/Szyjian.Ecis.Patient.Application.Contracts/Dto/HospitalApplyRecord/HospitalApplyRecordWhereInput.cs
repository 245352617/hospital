using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class HospitalApplyRecordWhereInput
    {
        public int Id { get; set; }

        /// <summary>
        /// 分诊患者id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }
    }
}