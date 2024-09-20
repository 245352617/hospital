namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 
/// </summary>
public class LisReportInfoResponse
{
    public List<ListApplyInfoResponse> ApplyInfos { get; set; }


    /// <summary>
    /// 条码号
    /// </summary>
    public string BarcodeNo { get; set; }

    /// <summary>
    /// 急诊标志		0普通;1急诊
    /// </summary>
    public string EmergencyFlag { get; set; }

    /// <summary>
    /// 报告单号
    /// </summary>
    public string ReportNo { get; set; }

    /// <summary>
    /// 报告标题
    /// </summary>
    public string ReportTitle { get; set; }

    /// <summary>
    /// 科目类别		生化，免疫，细菌…
    /// </summary>
    public string SubjectClass { get; set; }

    /// <summary>
    /// 检验目的
    /// </summary>
    public string LabPurpose { get; set; }

    /// <summary>
    /// 检验方法
    /// </summary>
    public string LabMethod { get; set; }

    /// <summary>
    /// 标本代码
    /// </summary>
    public string SpecimenCode { get; set; }

    /// <summary>
    /// 标本名称
    /// </summary>
    public string SpecimenName { get; set; }

    /// <summary>
    /// 采集部位
    /// </summary>
    public string SpecimenCollectPart { get; set; }

    /// <summary>
    /// 部位描述
    /// </summary>
    public string SpecimenCollectPartDesc { get; set; }

    /// <summary>
    /// 危险程度
    /// </summary>
    public string SpecimenRiskFactor { get; set; }

    /// <summary>
    /// 程度描述
    /// </summary>
    public string SpecimenRiskFactorDesc { get; set; }

    /// <summary>
    /// 标本质量
    /// </summary>
    public string SpecimenQuality { get; set; }

    /// <summary>
    /// 质量描述
    /// </summary>
    public string SpecimenQualityDesc { get; set; }

    /// <summary>
    /// 仪器编码
    /// </summary>
    public string LabInstrument { get; set; }

    /// <summary>
    /// 仪器名称
    /// </summary>
    public string LabInstrumentName { get; set; }

    /// <summary>
    /// 采样时间
    /// </summary>
    public string SpecimenCollectTime { get; set; }

    /// <summary>
    /// 采样人
    /// </summary>
    public string SpecimenCollectOperator { get; set; }

    /// <summary>
    /// 采样人编号
    /// </summary>
    public string SpecimenCollectOperatorCode { get; set; }

    /// <summary>
    /// 标本接收时间
    /// </summary>
    public string SpecimenAcceptTime { get; set; }

    /// <summary>
    /// 标本接收人
    /// </summary>
    public string SpecimenAcceptOperator { get; set; }

    /// <summary>
    /// 标本接收人编号
    /// </summary>
    public string SpecimenAcceptOperatorCode { get; set; }

    /// <summary>
    /// 检验时间
    /// </summary>
    public string LabTime { get; set; }

    /// <summary>
    /// 检验科室
    /// </summary>
    public string LabDept { get; set; }

    /// <summary>
    /// 检验医生
    /// </summary>
    public string LabOperator { get; set; }

    /// <summary>
    /// 报告时间
    /// </summary>
    public string ReportTime { get; set; }

    /// <summary>
    /// 报告人
    /// </summary>
    public string ReportOperator { get; set; }

    /// <summary>
    /// 报告人编号
    /// </summary>
    public string ReportOperatorCode { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    public string AuditTime { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    public string AuditOperator { get; set; }

    /// <summary>
    /// 审核人编号
    /// </summary>
    public string AuditOperatorCode { get; set; }

    /// <summary>
    /// 打印时间
    /// </summary>
    public string PrintTime { get; set; }

    /// <summary>
    /// 打印人
    /// </summary>
    public string PrintOperator { get; set; }

    /// <summary>
    /// 打印人编号
    /// </summary>
    public string PrintOperatorCode { get; set; }

    /// <summary>
    /// 模板编号
    /// </summary>
    public string ReportTemplateCode { get; set; }

    /// <summary>
    /// 报告url路径
    /// </summary>
    public string ReportUrl { get; set; }

    /// <summary>
    /// 报告类型		0:普通报告； 1：微生物报告
    /// </summary>
    public EReportType ReportType { get; set; }

    /// <summary>
    /// 微生物报告
    /// </summary>
    public List<LisReportItemsResponse> ReportItems { get; set; }


}
