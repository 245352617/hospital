namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：pda执行单执行记录同步eto
/// 创建人： yangkai
/// 创建时间：2022/11/28 14:20:54
/// </summary>
public class PdaExecuteRecordEto
{
    /// <summary>
    /// 患者id
    /// </summary>
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// 患者流水号
    /// </summary>
    public string PatientNo { get; set; } = string.Empty;

    /// <summary>
    /// 执行组内序号
    /// </summary>
    public string PlacerOrderNumber { get; set; } = string.Empty;

    /// <summary>
    /// 执行组号
    /// </summary>
    public string PlacerGroupNumber { get; set; } = string.Empty;

    /// <summary>
    /// 医嘱状态 4：已执行 5：拒执行 6：撤销执行
    /// </summary>
    public string OrderStatus { get; set; } = string.Empty;

    /// <summary>
    /// 拒执行原因 orderStatus为“拒执行”时,该字段必填,且不能为空字符串或空格,否则不予进行拒执行
    /// </summary>
    public string RefuseReason { get; set; } = string.Empty;

    /// <summary>
    /// 执行人编码
    /// </summary>
    public string StartNurseCode { get; set; } = string.Empty;

    /// <summary>
    /// 执行人名称
    /// </summary>
    public string StartNurseName { get; set; } = string.Empty;

    /// <summary>
    /// 预计执行时间 格式：yyyy-MM-dd HH:mm:ss
    /// </summary>
    public DateTime PlanExecTime { get; set; }

    /// <summary>
    /// 执行时间 格式：yyyy-MM-dd HH:mm:ss
    /// </summary>
    public DateTime? StartExecTime { get; set; }

    /// <summary>
    /// 审核人编码
    /// </summary>
    public string ReviewNurseCode { get; set; } = string.Empty;

    /// <summary>
    /// 审核人名称
    /// </summary>
    public string ReviewNurseName { get; set; } = string.Empty;

    /// <summary>
    /// 审核时间 格式：yyyy-MM-dd HH:mm:ss
    /// </summary>
    public string ReviewExecTime { get; set; } = string.Empty;
}
