namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 消息返回
/// </summary>
/// <typeparam name="T"></typeparam>
public class DoctorsAdviceReplyEto<T> where T : class, new()
{
    /// <summary>
    /// 源消息
    /// </summary>
    public T Data { get; set; }
    /// <summary>
    /// 返回处理代码
    /// </summary>
    public int Code { get; set; } = 200;
    /// <summary>
    /// 返回处理消息描述
    /// </summary>
    public string Message { get; set; } = "ok";
}
