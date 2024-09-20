using System.ComponentModel.DataAnnotations;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 检验报告列表查询
/// </summary>
public class GetLisReportListRequest
{
    /// <summary>
    /// 开始日期
    /// </summary> 
    public string BeginDate { get; set; }

    /// <summary>
    /// 结束日期
    /// </summary> 
    public string EndDate { get; set; }
    /// <summary>
    /// 患者ID
    /// </summary>
    [Required] 
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊号 住院号/门诊号
    /// </summary>
    [Required] 
    public string VisitNo { get; set; }

    /// <summary>
    /// 1门急诊 2住院 3体检
    /// </summary>
    [Required] 
    public string PatientType { get; set; }


}