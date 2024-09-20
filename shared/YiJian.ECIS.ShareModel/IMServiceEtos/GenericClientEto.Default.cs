namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 向指定 WebSocket 客户端发送消息的实体模型
/// </summary>
[Serializable]
public class DefaultClientEto : GenericClientEto<object>
{
    /// <summary>
    /// 
    /// </summary>
    public DefaultClientEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionId">WebSocket 客户端 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public DefaultClientEto(string connectionId, string method)
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
    public DefaultClientEto(string connectionId, string method, object data)
    {
        this.ConnectionId = connectionId;
        this.Method = method;
        this.Data = data;
    }
}
