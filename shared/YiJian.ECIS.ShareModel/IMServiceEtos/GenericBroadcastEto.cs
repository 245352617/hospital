namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 发送广播消息的实体模型
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericBroadcastEto<T>
{
    /// <summary>
    /// 
    /// </summary>
    public GenericBroadcastEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public GenericBroadcastEto(string method)
    {
        this.Method = method;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    /// <param name="data">向客户端发送的消息体</param>
    public GenericBroadcastEto(string method, T data)
    {
        this.Method = method;
        this.Data = data;
    }

    /// <summary>
    /// 调用的 WebSocket 方法名称
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// 向客户端发送的消息体
    /// </summary>
    public T Data { get; set; }
}
