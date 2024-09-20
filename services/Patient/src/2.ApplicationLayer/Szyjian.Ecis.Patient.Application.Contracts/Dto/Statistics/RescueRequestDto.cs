using System;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class RescueRequestDto
    {
        public int Index { get; set; } = 1;

        public int PageCount { get; set; } = 10;

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public string DeptCode { get; set; }

        public int VisitState { get; set; }

        public string InpatientName { get; set; }

        public string Search { get; set; }

        public bool? IsGreen { get; set; }
    }
}
