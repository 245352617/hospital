namespace HisApiMockService.Models.Medicals;
 
/// <summary>
/// 微生物报告
/// </summary>
public class LisReportItemsResponse
{
    /// <summary>
    /// 正常报告
    /// </summary>
    public LisReportItemInfo ReportItem { get;set; }

    /// <summary>
    /// 微生物报告
    /// </summary>
    public LisMicroReportItemInfo MicroReportItem { get; set; }

}

