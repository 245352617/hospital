namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 发送广播消息的实体模型
/// </summary>
public class DefaultBroadcastEto : GenericBroadcastEto<object>
{
    /// <summary>
    /// 
    /// </summary>
    public DefaultBroadcastEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public DefaultBroadcastEto(string method)
    {
        this.Method = method;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    /// <param name="data">向客户端发送的消息体</param>
    public DefaultBroadcastEto(string method, object data)
    {
        this.Method = method;
        this.Data = data;
    }
}
