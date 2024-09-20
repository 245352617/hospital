using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 查询检查报告列表
    /// </summary>
    public class QueryPacsReportListResponse : PatientInfoResponse
    {
        /// <summary>
        /// 报表信息
        /// </summary>
        public List<ReportInfoListResponse> ReportInfos { get; set; } = new List<ReportInfoListResponse>();

    }

}