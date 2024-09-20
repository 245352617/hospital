namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：pda回传执行结果
/// 创建人： yangkai
/// 创建时间：2022/12/2 11:22:46
/// </summary>
public class ReceiveExecuteResultEto
{
    /// <summary>
    /// 医嘱组号
    /// </summary>
    public Guid PlacerGroupNumber { get; set; }

    /// <summary>
    /// 护士编码
    /// </summary>
    public string NurseCode { get; set; } = string.Empty;

    /// <summary>
    /// 护士名称
    /// </summary>
    public string NurseName { get; set; } = string.Empty;

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime Optime { get; set; }

    /// <summary>
    /// 操作类型
    /// Execute:执行
    /// Cancel:取消
    /// </summary>
    public string OptType { get; set; } = string.Empty;
}
