namespace YiJian.ECIS.ShareModel.DDPs.Responses;

/// <summary>
/// 检验报告列表查询
/// </summary>
public class DdpGetLisReportListResponse
{

    /// <summary>
    /// 检验项目代码
    /// </summary>
    public string MasterItemCode { get; set; }

    /// <summary>
    /// 检验项目名称[展示]
    /// </summary>
    public string MasterItemName { get; set; }

    /// <summary>
    /// 报告No.
    /// </summary>
    public string ReportNo { get; set; }

    /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? LabTime { get; set; }

    /// <summary>
    /// 检验项
    /// </summary> 
    //public List<DdpGetLisReportResponse> LisReportList { get; set; }
}
