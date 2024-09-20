using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 用于和医嘱服务交互的作废消息和执行消息，另外停嘱采用停嘱Eto。
/// </summary> 
public class DoctorsAdviceStatusEto
{
    /// <summary>
    /// 系统标识：0=急诊、1=院前急救
    /// </summary>
    public int PlatformType { get; set; }

    /// <summary>
    /// 病人ID
    /// </summary>
    public Guid PIID { get; set; }

    /// <summary>
    /// 医嘱ID集合
    /// </summary>
    public List<Guid> RecipeIds { get; set; }

    /// <summary>
    /// 操作类型
    /// </summary>
    public EDoctorsAdviceOperation Operation { get; set; }

    /// <summary>
    /// 医生/护士编码
    /// </summary>
    public string OperatorCode { get; set; }

    /// <summary>
    /// 医生/护士名称
    /// </summary>
    public string OperatorName { get; set; }

    /// <summary>
    /// 操作时间
    /// </summary>
    public DateTime Optime { get; set; }

    /// <summary>
    /// 医嘱状态
    /// </summary>
    public EDoctorsAdviceStatus Status { get; set; }

}
