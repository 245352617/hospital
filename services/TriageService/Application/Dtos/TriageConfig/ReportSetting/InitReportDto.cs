using System.Collections.Generic;
using System.Data;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 报表初始化Dto
    /// </summary>
    public class InitReportDto
    {
        /// <summary>
        /// 报表表头
        /// </summary>
        public string ReportHead { get; set; }

        public ICollection<InitReportQueryDto> InitReportQuery { get; set; }
    }

    /// <summary>
    /// 报表初始化查询项Dto
    /// </summary>
    public class InitReportQueryDto
    {
        /// <summary>
        /// 查询名称
        /// </summary>
        public string QueryName { get; set; }

        /// <summary>
        /// 查询字段
        /// </summary>
        public string QueryFiled { get; set; }

        /// <summary>
        /// 查询控件类型
        /// </summary>
        public string QueryType { get; set; }

        /// <summary>
        /// 查询选项数据
        /// </summary>
        public DataTable QueryData { get; set; }

        /// <summary>
        /// 默认值
        /// </summary>
        public string DefaultValue { get; set; }
    }
}