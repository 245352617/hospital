namespace YiJian.ECIS.ShareModel.Etos;

/// <summary>
/// 描述：pda队列消息基础包装类
/// 创建人： yangkai
/// 创建时间：2022/11/28 10:01:46
/// </summary>
public class PdaBaseEto<T> where T : class
{
    /// <summary>
    /// 系统代码
    /// </summary>
    public string Syscode { get; set; } = "ECIS";

    /// <summary>
    /// 事件代码
    /// </summary>
    public string Eventcode { get; set; }

    /// <summary>
    /// 时间戳
    /// </summary>
    public DateTime @DateTime { get; set; }

    /// <summary>
    /// 参数体
    /// </summary>
    public T Body { get; set; }
}
