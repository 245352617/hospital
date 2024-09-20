namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 查询患者信息入参Dto
    /// </summary>
    public class PatientInfoRepDto
    {
        /// <summary>
        /// 患者标识、患者编号
        /// </summary>
        public string PatientId { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string IDNumber { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string PhoneNum { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }
    }
}