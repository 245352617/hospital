namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 查找存在过分诊记录患者Dto
    /// </summary>
    public class SelectAlreadyTriageDto
    {
        /// <summary>
        /// 身份证号
        /// </summary>
        public string IdentityNo { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 联系电话
        /// </summary>
        public string Phone { get; set; }
    }
}