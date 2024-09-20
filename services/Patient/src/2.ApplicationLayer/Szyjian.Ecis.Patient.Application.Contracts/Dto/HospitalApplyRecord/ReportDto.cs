using System.Collections.Generic;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class ReportDto
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public List<AdmissionRecordDto> AdmissionRecords { get; set; }

        /// <summary>
        /// 入院信息
        /// </summary>
        public List<HospitalApplyRecordDto> HospitalApplyRecord { get; set; }

    }
}