using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class Infusion1RequestDto
    {
        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string DoctorName { get; set; }
    }
}
