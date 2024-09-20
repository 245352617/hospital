namespace HisApiMockService.Models.Medicals;
/// <summary>
/// 申请单
/// </summary>
public class LisReportApplyInfoResponse
{
    /// <summary>
    /// 申请单号  多个申请单用分号隔开；
    /// </summary>
    public string ApplyNo { get; set; }

    /// <summary>
    /// 申请人编号
    /// </summary>
    public string ApplyOperatorCode { get; set; }

    /// <summary>
    /// 申请人姓名
    /// </summary>
    public string ApplyOperatorName { get; set; }

    /// <summary>
    /// 申请科室编号
    /// </summary>
    public string ApplyDeptCode { get; set; }

    /// <summary>
    /// 申请科室名称
    /// </summary>
    public string ApplyDeptName { get; set; }

    /// <summary>
    /// 就诊科室编号
    /// </summary>
    public string VisitDeptCode { get; set; }

    /// <summary>
    /// 就诊科室名称
    /// </summary>
    public string VisitDeptName { get; set; }

    /// <summary>
    /// 申请时间
    /// </summary>
    public DateTime? ApplyTime { get; set; }

    /// <summary>
    /// 
    /// </summary> 
    public List<ReportMasterItemResponse> MasterItemList { get; set; }

}
