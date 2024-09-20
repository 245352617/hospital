namespace YiJian.ECIS.ShareModel.Etos.ReportHistoryDatas;

/// <summary>
/// 
/// </summary>
public class ReportDataEto
{
    /// <summary>
    /// 患者分诊id
    /// </summary>
    public Guid PIID { get; set; }

    /// <summary>
    /// 模板id
    /// </summary>
    public Guid TempId { get; set; }

    /// <summary>
    /// 处方号
    /// </summary>
    public string PrescriptionNo { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public string DataContent { get; set; }

    /// <summary>
    /// 操作人编码
    /// </summary>
    public string OperationCode { get; set; }
}