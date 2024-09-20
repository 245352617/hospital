namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class Infusion1ResponseDto
    {
        public string FirstDoctorCode { get; set; }

        public string FirstDoctorName { get; set; }

        public int Total { get; set; }

        public int InfusionCount { get; set; }

        public decimal Percentage { get { return InfusionCount / (decimal)Total; } }
    }
}
