namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PatientInfoChangeInput
    {
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = int.MaxValue;

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
    }
}
