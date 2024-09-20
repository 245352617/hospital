namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    /// <summary>
    /// 转诊
    /// Directory: input
    /// </summary>
    public class CallReferralDto
    {
        /// <summary>
        /// （需要修改的数据的根据）就诊号 挂号之后生成就诊号
        /// </summary>
        public string RegisterNo { get; set; }

        /// <summary>
        /// 患者唯一标识(HIS)
        /// </summary>
        public string PatientID { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 医生 ID
        /// </summary>
        public string DoctorId { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        ///  分诊级别
        /// </summary> 
        public string TriageLevel { get; set; }

        /// <summary>
        /// 实际 分诊级别
        /// </summary>
        public string TriageLevelName { get; set; }

        /// <summary>
        /// 分诊去向Code
        /// </summary>
        public string TriageTarget { get; set; }

        /// <summary>
        /// 分诊去向Name
        /// </summary>
        public string TriageTargetName { get; set; }
    }
}
