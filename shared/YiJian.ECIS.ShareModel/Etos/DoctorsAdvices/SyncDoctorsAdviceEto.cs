using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 已驳回,已确认,已执行
/// </summary>
public class SyncDoctorsAdviceEto
{
    /// <summary>
    /// 医嘱Id
    /// </summary>
    public List<Guid> Ids { get; set; }

    /// <summary>
    /// 操作人编码
    /// </summary> 
    public string OperationCode { get; set; }

    /// <summary>
    /// 操作人名称
    /// </summary> 
    public string OperationName { get; set; }

    /// <summary>
    /// 操作时间（驳回时间，复核时间，执行时间）
    /// </summary> 
    public DateTime OperationTime { get; set; }

    /// <summary>
    /// 医嘱执行状态
    /// </summary>
    public EDoctorsAdviceStatus DoctorsAdviceStatus { get; set; }

}
