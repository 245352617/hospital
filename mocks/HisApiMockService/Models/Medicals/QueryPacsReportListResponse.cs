namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 查询检查报告列表
/// </summary>
public class QueryPacsReportListResponse
{
    /// <summary>
    /// 病人ID HIS的病人ID
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊号 门诊号/住院号
    /// </summary>
    public string VisitNo { get; set; }

    /// <summary>
    /// 就诊类型
    /// </summary>
    public EVisitType VisitType { get; set; }

    /// <summary>
    /// 姓名 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime PatientBirthDay { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public string PatientAge { get; set; }

    /// <summary>
    /// 床号
    /// </summary>
    public string WholeOrganizationId { get; set; }

    /// <summary>
    /// 报表信息
    /// </summary>
    public List<ReportInfoListResponse> ReportInfos { get; set; } = new List<ReportInfoListResponse>();

}
