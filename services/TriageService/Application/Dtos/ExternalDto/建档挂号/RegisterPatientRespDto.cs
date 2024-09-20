namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者挂号出参Dto
    /// </summary>
    public class RegisterPatientRespDto
    {
        /// <summary>
        /// 患者编号，患者标识
        /// </summary>
        public string PatientId { get; set; }
        
        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }
        
        /// <summary>
        /// 就诊时间
        /// </summary>
        public string VisitDate { get; set; }
        
        /// <summary>
        /// 挂号标识
        /// </summary>
        public string RegisterId { get; set; }
        
        /// <summary>
        /// 挂号时间
        /// </summary>
        public string RegistDate { get; set; }
    }
}