using System.ComponentModel.DataAnnotations;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 检验报告详情查询
/// </summary>
public class GetLisReportRequest
{
    /// <summary>
    /// 患者ID
    /// </summary>
    [Required] 
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊类型		1门急诊2住院 3体检
    /// </summary> 
    public string PatientType { get; set; }

    /// <summary>
    /// 就诊号
    /// </summary> 
    public string VisitNo { get; set; }

    /// <summary>
    /// 申请单号
    /// </summary> 
    public string ApplyNo { get; set; }

    /// <summary>
    /// 报告单号
    /// </summary>
    [Required] 
    public string ReportNo { get; set; }

}