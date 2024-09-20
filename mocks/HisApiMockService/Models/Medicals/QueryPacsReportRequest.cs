using System.ComponentModel.DataAnnotations;

namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 查询检查报告信息请求参数
/// </summary>
public class QueryPacsReportRequest
{
    /// <summary>
    /// 患者ID
    /// </summary>
    [Required] 
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊类型 1门急诊;2住院; 3体检
    /// </summary>
    [Required] 
    public int PatientType { get; set; }

    /// <summary>
    /// 就诊号
    /// </summary>
    [Required] 
    public string VisitNo { get; set; }

    /// <summary>
    /// 就诊流水号  门诊流水号/住院流水号  就诊唯一号
    /// </summary>  
    public string VisitSerialNo { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    [Required] 
    public string Name { get; set; }

    /// <summary>
    /// 申请单号
    /// </summary> 
    public string ApplyNo { get; set; }

    /// <summary>
    /// 检查类型 1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
    /// </summary> 
    public string ExamType { get; set; }

}
