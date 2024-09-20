namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 报表信息
/// </summary>
public class ReportInfoSampleResponse
{
    /// <summary>
    /// 检查类型 1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
    /// </summary>
    public EExamType ExamType { get; set; }

    /// <summary>
    /// 申请单号
    /// </summary>
    public string ApplyNo { get; set; }

    /// <summary>
    /// 检查号
    /// </summary>
    public string StudyId { get; set; }

    /// <summary>
    /// 检查项目代码
    /// </summary>
    public string ItemCode { get; set; }

    /// <summary>
    /// 检查项目名称
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// 报告单号
    /// </summary>
    public string ReportNo { get; set; }

    /// <summary>
    /// 报告标题
    /// </summary>
    public string ReportTitle { get; set; }

    /// <summary>
    /// 报告类别
    /// </summary>
    public string SubjectClass { get; set; }
}
