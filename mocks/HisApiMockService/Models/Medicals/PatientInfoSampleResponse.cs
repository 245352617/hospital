namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 查询检查报告列表患者信息
/// </summary>
public class PatientInfoSampleResponse
{
    /// <summary>
    /// 病人ID   HIS的病人ID
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 就诊号  门诊号/住院号
    /// </summary>
    public string VisitNo { get; set; }

    /// <summary>
    /// 就诊类型 患者类型代码--CV09.00.404 1.门诊 2.急诊 3.住院 9.体检（其他）
    /// </summary>
    public EVisitType VisitType { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string Gender { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PatientBirthDay { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string PatientAge { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string WholeOrganizationId { get; set; }
}
