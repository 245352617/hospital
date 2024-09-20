namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 推送患者分诊去向信息队列Dto
    /// </summary>
    public class ConsequenceInfoMqDto
    {
        /// <summary>
        /// 分诊科室编码
        /// </summary>
        public string TriageDeptCode { get; set; }

        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }

        /// <summary>
        /// 分诊去向编码
        /// </summary>
        public string TriageTargetCode { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }
    }
}