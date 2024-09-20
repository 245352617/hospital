using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 分诊工作量统计
    /// </summary>
    public class WorkLoadStatisticsDto
    {
        /// <summary>
        /// 表头
        /// </summary>
        public List<ReportHeaderDto> Header { get; set; }

        /// <summary>
        /// 表体
        /// </summary>
        public List<WorkLoadInfoDto> Body { get; set; }
        /// <summary>
        /// 总数
        /// </summary>
        public int TotalCount { get; set; }
    }
}