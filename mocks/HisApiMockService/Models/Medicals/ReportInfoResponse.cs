namespace HisApiMockService.Models.Medicals;

/// <summary>
/// 报表信息
/// </summary>
public class ReportInfoResponse
{
    /// <summary>
    /// 检查类型 	1:RIS放射;2:US超声; 3:ES内镜;4:病理PAT; 5:心电ECG;
    /// </summary>
    public EExamType ExamType { get; set; }

    /// <summary>
    /// 申请单号
    /// </summary>
    public string ApplyNo { get; set; }

    /// <summary>
    /// 医嘱号
    /// </summary>
    public string PrescrobeNo { get; set; }

    /// <summary>
    /// 检查号
    /// </summary>
    public string StudyId { get; set; }

    /// <summary>
    /// 申请科室代码
    /// </summary>
    public string ApplyDeptCode { get; set; }

    /// <summary>
    /// 申请科室名称
    /// </summary>
    public string ApplyDeptName { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    public string ApplyTime { get; set; }

    /// <summary>
    /// 申请医生
    /// </summary>
    public string ApplyOperatorName { get; set; }

    /// <summary>
    /// 申请医生编号
    /// </summary>
    public string ApplyOperatorCode { get; set; }

    /// <summary>
    /// 检查项目代码
    /// </summary>
    public string ItemCode { get; set; }

    /// <summary>
    /// 检查项目名称
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// 检查部位
    /// </summary>
    public string ExamPart { get; set; }

    /// <summary>
    /// 检查部位描述
    /// </summary>
    public string ExamPartDesc { get; set; }

    /// <summary>
    /// 检查目的
    /// </summary>
    public string ExamPurpose { get; set; }

    /// <summary>
    /// 病情描述
    /// </summary>
    public string VisitStateDesc { get; set; }

    /// <summary>
    /// 报告单号
    /// </summary>
    public string ReportNo { get; set; }

    /// <summary>
    /// 报告标题
    /// </summary>
    public string ReportTitle { get; set; }

    /// <summary>
    /// 类别
    /// </summary>
    public string SubjectClass { get; set; }

    /// <summary>
    /// 检查所见
    /// </summary>
    public string StudySee { get; set; }

    /// <summary>
    /// 检查提示		(检查结论)
    /// </summary>
    public string StudyHint { get; set; }

    /// <summary>
    /// 印象
    /// </summary>
    public string Impression { get; set; }

    /// <summary>
    /// 建议
    /// </summary>
    public string Recommendation { get; set; }

    /// <summary>
    /// 检查设备
    /// </summary>
    public string Modality { get; set; }

    /// <summary>
    /// 检查设备名称
    /// </summary>
    public string ModalityName { get; set; }

    /// <summary>
    /// 报告中图象编号
    /// </summary>
    public string UseImage { get; set; }

    /// <summary>
    /// PDF报告Url地址 没有PDF报告可不提供
    /// </summary>
    public string ReportPdfurl { get; set; }

    /// <summary>
    /// 异常标志		0正常1异常
    /// </summary>
    public string AbnormalFlag { get; set; }

    /// <summary>
    /// 检查方法
    /// </summary>
    public string ExamMethod { get; set; }

    /// <summary>
    /// 检查时间
    /// </summary>
    public string ParticipantTime { get; set; }

    /// <summary>
    /// 检查科室
    /// </summary>
    public string AssociatedEntityName { get; set; }

    /// <summary>
    /// 检查人编号 examOperatorCode
    /// </summary>
    public string AuthenticatorCode { get; set; }

    /// <summary>
    /// 检查人 examOperatorName
    /// </summary>
    public string AuthenticatorName { get; set; }

    /// <summary>
    /// 登记时间
    /// </summary>
    public string RegisterTime { get; set; }

    /// <summary>
    /// 登记人编号
    /// </summary>
    public string RegisterOperatorCode { get; set; }

    /// <summary>
    /// 登记人
    /// </summary>
    public string RegisterOperatorName { get; set; }

    /// <summary>
    /// 报告时间
    /// </summary>
    public string AuthorTime { get; set; }

    /// <summary>
    /// 报告人编号
    /// </summary>
    public string AssignedAuthorId { get; set; }

    /// <summary>
    /// 报告人
    /// </summary>
    public string AssignedPersonName { get; set; }

    /// <summary>
    /// 审核时间
    /// </summary>
    public string AuditTime { get; set; }

    /// <summary>
    /// 审核人编号
    /// </summary>
    public string LegalAuthenticatorCode { get; set; }

    /// <summary>
    /// 审核人
    /// </summary>
    public string LegalAuthenticatorName { get; set; }

    /// <summary>
    /// 打印时间
    /// </summary>
    public string PrintTime { get; set; }

    /// <summary>
    /// 打印人员编号
    /// </summary>
    public string PrintOperatorCode { get; set; }

    /// <summary>
    /// 打印人员
    /// </summary>
    public string PrintOperatorName { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 病理号
    /// </summary>
    public string TctId { get; set; }

    /// <summary>
    /// 冰冻号
    /// </summary>
    public string IceNo { get; set; }

    /// <summary>
    /// 送检日期
    /// </summary>
    public string SendTime { get; set; }

    /// <summary>
    /// 送检材料
    /// </summary>
    public string SendItem { get; set; }

    /// <summary>
    /// 免疫组化
    /// </summary>
    public string Ihc { get; set; }

    /// <summary>
    /// 病理诊断
    /// </summary>
    public string TctReport { get; set; }

}
