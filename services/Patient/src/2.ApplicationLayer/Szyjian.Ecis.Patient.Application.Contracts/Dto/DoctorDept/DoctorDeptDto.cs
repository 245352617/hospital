namespace Szyjian.Ecis.Patient.Application.Contracts
{
    /// <summary>
    /// 医生的科室诊室
    /// </summary>
    public class DoctorDeptDto
    {

        /// <summary>
        /// 科室
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 诊室
        /// </summary>
        public string Room { get; set; }
    }
}