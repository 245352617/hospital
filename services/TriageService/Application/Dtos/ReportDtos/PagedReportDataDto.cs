using System.Data;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class PagedReportDataDto
    {
        /// <summary>
        /// 总条数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public DataTable Dt { get; set; } = new DataTable();
    }
}