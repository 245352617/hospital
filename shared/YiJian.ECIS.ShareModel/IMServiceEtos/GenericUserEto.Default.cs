namespace YiJian.ECIS.ShareModel.IMServiceEto;

/// <summary>
/// 通过 IMService 向指定用户的所有 WebSocket 终端发送消息的实体模型
/// IMService 暂未实现用户认证，该方法暂时无法使用  by: ywlin-20211026
/// </summary>
public sealed class DefaultUserEto : GenericUserEto<object>
{
    /// <summary>
    /// 
    /// </summary>
    public DefaultUserEto()
    {
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="userId">用户 ID</param>
    /// <param name="method">调用的 WebSocket 方法名称</param>
    public DefaultUserEto(string userId, string method)
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
    public DefaultUserEto(string userId, string method, object data)
    {
        this.UserId = userId;
        this.Method = method;
        this.Data = data;
    }
}
