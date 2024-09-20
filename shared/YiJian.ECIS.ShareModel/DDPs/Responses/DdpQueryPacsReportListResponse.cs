namespace YiJian.ECIS.ShareModel.DDPs.Responses;

/// <summary>
/// 查询检查报告列表
/// </summary>
public class DdpQueryPacsReportListResponse
{
    /// <summary>
    /// 申请单号[需要展示]
    /// </summary>
    public string ApplyNo { get; set; } = string.Empty;

    /// <summary>
    /// 检查项目代码
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 检查项目名称[需要展示]
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 检查时间
    /// </summary>
    public DateTime? ExamTime { get; set; }

    /// <summary>
    /// url
    /// </summary>
    public string Url { get; set; }
}
