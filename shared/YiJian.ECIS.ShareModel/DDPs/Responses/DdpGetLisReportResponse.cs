namespace YiJian.ECIS.ShareModel.DDPs.Responses;

/// <summary>
/// 检验报告详情查询
/// </summary>
public class DdpGetLisReportResponse
{
    /// <summary>
    /// 细项代码
    /// </summary>
    public string ItemCode { get; set; }

    /// <summary>
    /// 细项名称 [指标名称]
    /// </summary>
    public string ItemChiName { get; set; }

    /// <summary>
    /// 结果值
    /// </summary>
    public string ItemResult { get; set; }

    /// <summary>
    /// 结果单位
    /// </summary>
    public string ItemResultUnit { get; set; }

    /// <summary>
    /// 结果值标志 N正常;L偏低;H偏高
    /// </summary>
    public string ItemResultFlag { get; set; }

    /// <summary>
    /// 参考值描述
    /// </summary>
    public string ReferenceDesc { get; set; }

    /// <summary>
    /// 参考值上限
    /// </summary>
    public string ReferenceHighLimit { get; set; }

    /// <summary>
    /// 参考值下限
    /// </summary>
    public string ReferenceLowLimit { get; set; }

}
