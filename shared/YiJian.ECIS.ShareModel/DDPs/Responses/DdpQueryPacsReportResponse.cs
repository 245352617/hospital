namespace YiJian.ECIS.ShareModel.DDPs.Responses;


/// <summary>
/// 查询检查报告信息
/// </summary>
public class DdpQueryPacsReportResponse
{
    /// <summary>
    /// 检查项目代码
    /// </summary>
    public string ItemCode { get; set; } = string.Empty;

    /// <summary>
    /// 检查项目名称
    /// </summary>
    public string ItemName { get; set; } = string.Empty;

    /// <summary>
    /// 检查所见
    /// </summary>
    public string StudySee { get; set; } = string.Empty;

    /// <summary>
    /// 检查提示		(检查结论)
    /// </summary>
    public string StudyHint { get; set; } = string.Empty;

    /// <summary>
    /// 检查时间
    /// </summary>
    public DateTime ParticipantTime { get; set; }
}
