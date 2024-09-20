namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 患者挂号入参Dto
    /// </summary>
    public class RegisterPatientRepDto
    {
        /// <summary>
        /// 患者编号
        /// </summary>
        public string PatientId { get; set; }
        
        /// <summary>
        /// 费别
        /// </summary>
        public string ChargeType { get; set; }
        
        /// <summary>
        /// 挂号时间
        /// </summary>
        public string RegisterDate { get; set; }
        
        /// <summary>
        /// 挂号科别代码
        /// </summary>
        public string DeptCode { get; set; }
        
        /// <summary>
        /// 医生代码
        /// </summary>
        public string DoctorCode { get; set; }
        
        /// <summary>
        /// 挂号有效期：上午/下午/全天
        /// </summary>
        public string Shift { get; set; }
        
        /// <summary>
        /// 登记窗口
        /// </summary>
        public string SiteCode { get; set; }
        
        /// <summary>
        /// 操作员
        /// </summary>
        public string Operator { get; set; }

    }
}