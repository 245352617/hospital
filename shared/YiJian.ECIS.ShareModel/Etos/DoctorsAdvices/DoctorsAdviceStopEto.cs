namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 停嘱
/// </summary>
public class DoctorsAdviceStopEto : DoctorsAdviceStatusEto
{
    /// <summary>
    /// 停嘱时间
    /// </summary>
    public DateTime StopTime { get; set; }
}
