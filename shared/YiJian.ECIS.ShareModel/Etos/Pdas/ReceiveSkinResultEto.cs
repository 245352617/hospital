namespace YiJian.ECIS.ShareModel.Etos.Pdas;

/// <summary>
/// 描述：pda回传皮试结果
/// 创建人： yangkai
/// 创建时间：2022/12/2 14:40:51
/// </summary>
public class ReceiveSkinResultEto
{
    /// <summary>
    /// 医嘱ID
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// 皮试结果
    /// </summary>
    public bool SkinTestResult { get; set; }

    /// <summary>
    /// 皮试执行时间
    /// </summary>
    public DateTime SkinTestTime { get; set; }

    /// <summary>
    /// 护士编码
    /// </summary>
    public string NurseCode { get; set; }

    /// <summary>
    /// 护士名称
    /// </summary>
    public string NurseName { get; set; }
}
