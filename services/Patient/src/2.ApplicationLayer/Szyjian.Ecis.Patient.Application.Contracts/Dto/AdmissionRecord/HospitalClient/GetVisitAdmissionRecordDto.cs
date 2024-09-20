using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class GetVisitAdmissionRecordDto
    {
        public Guid PI_ID { get; set; }

        public string PatientID { get; set; }

        public string VisitNo { get; set; }
        public string VisSerialNo { get; set; }

        public DateTime? VisitDate { get; set; }
    }
}
