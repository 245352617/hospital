using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class TraumaPatientsRequestDto
    {
        public int Index { get; set; } = 1;

        public int PageCount { get; set; } = 10;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

    }

    public class TraumaProjectRequestDto : TraumaPatientsRequestDto
    {
        public string DoctorCode { get; set; }

        public string SearchText { get; set; }

        public string ProjectName { get; set; }
    }
}
