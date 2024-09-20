namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 向指定用户的所有 WebSocket 终端发送消息的实体模型
/// IMService 暂未实现用户认证，该方法暂时无法使用  by: ywlin-20211026
/// </summary>
/// <typeparam name="T"></typeparam>
public class GenericUserEto<T>
{
    /// <summary>
    /// 
    /// </summary>
    public GenericUserEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public GenericUserEto(string userId, string method)
    {
        this.UserId = userId;
        this.Method = method;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    /// <param name="data">消息体</param>
    public GenericUserEto(string userId, string method, T data)
    {
        this.UserId = userId;
        this.Method = method;
        this.Data = data;
    }

    /// <summary>
    /// 用户 ID
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    /// 调用的 WebSocket 方法名称
    /// </summary>
    public string Method { get; set; }

    /// <summary>
    /// 向客户端发送的消息体
    /// </summary>
    public T Data { get; set; }
}
