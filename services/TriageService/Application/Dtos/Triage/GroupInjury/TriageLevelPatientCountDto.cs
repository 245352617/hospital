namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 群伤分诊级别人数Dto
    /// </summary>
    public class TriageLevelPatientCountDto
    {
        /// <summary>
        /// 分诊级别Code
        /// </summary>
        public string TriageLevelCode { get; set; }

        /// <summary>
        /// 患者人数
        /// </summary>
        public int PatientCount { get; set; }

        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 挂号医生编码
        /// </summary>
        public string RegisterDocCode { get; set; }

        /// <summary>
        /// 挂号医生姓名
        /// </summary>
        public string RegisterDocName { get; set; }
    }
}