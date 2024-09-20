using System.Collections.Generic;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表表头Dto
    /// </summary>
    public class ReportHeaderDto
    {
        /// <summary>
        /// 表头名称
        /// </summary>
        public string HeaderName { get; set; }

        /// <summary>
        /// 表头字段名
        /// </summary>
        public string HeaderField { get; set; }

        /// <summary>
        /// 列宽
        /// </summary>
        public string HeaderWidth { get; set; }

        /// <summary>
        /// 表头序号
        /// </summary>
        public string HeaderSort { get; set; }

        /// <summary>
        /// 子级表头
        /// </summary>
        public ICollection<ReportHeaderDto> Children { get; set; }
    }
}