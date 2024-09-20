namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 创建或更新判定依据类型dto
    /// </summary>
    public class CreateOrUpdateJudgmentTypeDto
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 分诊科室
        /// </summary>
        public string TriageDeptCode { get; set; }
        
        /// <summary>
        /// 是否启用：false：不启用；true：启用
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
    }
}