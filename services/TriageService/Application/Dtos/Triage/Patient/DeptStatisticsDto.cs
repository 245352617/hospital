namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 科室挂号统计
    /// </summary>
    public class DeptStatisticsDto
    {
        /// <summary>
        /// 科室编码
        /// </summary>
        public string Dept { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 候诊总人数
        /// </summary>
        public int TotalWaitingCount { get; set; }

        /// <summary>
        /// 已就诊总人数
        /// </summary>
        public int TotalTreatedCount { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; }

    }
}
