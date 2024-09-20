namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 入叫号队列 
    /// </summary>
    public class InQueueCallInfoEto
    {
        /// <summary>
        /// 就诊号
        /// 挂号之后生成就诊号
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
        /// 挂号医生编码(如果传递的话就只能这个医生接诊)
        /// </summary> 
        public string DoctorCode { get; set; }

        /// <summary>
        /// 挂号医生姓名(如果传递的话就只能这个医生接诊)
        /// </summary> 
        public string DoctorName { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }

        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        public string ActTriageLevelName { get; set; }

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