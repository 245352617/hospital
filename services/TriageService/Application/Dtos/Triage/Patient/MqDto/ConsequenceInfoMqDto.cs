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
        /// HIS 的科室编码
        /// </summary>
        public string HisDeptCode { get; set; }
        
        /// <summary>
        /// 分诊科室名称
        /// </summary>
        public string TriageDeptName { get; set; }
        
        /// <summary>
        /// 分诊科室变更Code
        /// </summary>
        public string ChangeDept { get; set; }
        
        /// <summary>
        /// 分诊科室变更Name
        /// </summary>
        public string ChangeDeptName { get; set; }
        
        /// <summary>
        /// 分诊去向编码
        /// </summary>
        public string TriageTargetCode { get; set; }
        
        /// <summary>
        /// 分诊去向名称
        /// </summary>
        public string TriageTargetName { get; set; }

        /// <summary>
        /// 实际分诊级别
        /// </summary>
        public string ActTriageLevel { get; set; }
        
        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        public string ActTriageLevelName { get; set; }

        /// <summary>
        /// 自动分诊级别
        /// </summary>
        public string AutoTriageLevel { get; set; }
        
        /// <summary>
        /// 实际分诊级别名称
        /// </summary>
        public string AutoTriageLevelName { get; set; }

        /// <summary>
        /// 分诊级别变更
        /// </summary>
        public string ChangeLevel { get; set; }

        /// <summary>
        /// 分诊级别变更名称
        /// </summary>
        public string ChangeLevelName { get; set; }
    }
}