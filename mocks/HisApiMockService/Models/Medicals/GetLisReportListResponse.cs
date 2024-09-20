namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 检验报告列表查询
/// </summary>
public class GetLisReportListResponse
{
    /// <summary>
    /// 患者ID
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string EmpiId { get; set; }

    /// <summary>
    /// 病人类别
    /// </summary>
    public EPatientType PatientType { get; set; }

    /// <summary>
    /// 就诊号  门诊挂号号/住院号
    /// </summary>
    public string VisitNo { get; set; }

    /// <summary>
    /// 就诊流水号  就诊唯一号
    /// </summary>
    public string VisitSerialNo { get; set; }

    /// <summary>
    /// 姓名
    /// </summary>
    public string PatientName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string PatientGender { get; set; }

    /// <summary>
    /// 条码号
    /// </summary>
    public string BarcodeNo { get; set; }

    /// <summary>
    /// 报告单号
    /// </summary>
    public string ReportNo { get; set; }

    /// <summary>
    /// 检验时间
    /// </summary>
    public DateTime? LabTime { get; set; }

    /// <summary>
    /// 检验科室编码
    /// </summary>
    public string LabDeptCode { get; set; }

    /// <summary>
    /// 检验科室名称
    /// </summary>
    public string LabDeptName { get; set; }

    /// <summary>
    /// 检验医生编码
    /// </summary>
    public string LabOperatorCode { get; set; }

    /// <summary>
    /// 检验医生名称
    /// </summary>
    public string LabOperatorName { get; set; }

    /// <summary>
    /// 报告类型 0:普通报告；1：微生物报告
    /// </summary>
    public EReportType? ReportType { get; set; }

    /// <summary>
    /// 
    /// </summary> 
    public List<LisReportApplyInfoResponse> ApplyInfoList { get; set; }

}
