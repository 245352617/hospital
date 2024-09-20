namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 向指定 WebSocket 客户端发送消息的实体模型
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericClientEto<T>
{
    /// <summary>
    /// 
    /// </summary>
    public GenericClientEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionId">WebSocket 客户端 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public GenericClientEto(string connectionId, string method)
    {
        this.ConnectionId = connectionId;
        this.Method = method;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionId">WebSocket 客户端 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    /// <param name="data">消息体</param>
    public GenericClientEto(string connectionId, string method, T data)
    {
        this.ConnectionId = connectionId;
        this.Method = method;
        this.Data = data;
    }

    /// <summary>
    /// WebSocket 客户端连接 ID
    /// </summary>
    public string ConnectionId { get; set; }

    /// <summary>
    /// 调用的 WebSocket 方法名称
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// 向客户端发送的消息体
    /// </summary>
    public T Data { get; set; }
}
