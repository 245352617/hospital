using Volo.Abp;

namespace YiJian.ECIS.ShareModel.Exceptions;

/// <summary>
/// ECIS 通用异常类
/// </summary>
public class EcisBusinessException : BusinessException, IUserFriendlyException
{
    /// <summary>
    /// 数据
    /// </summary>
    public new object Data { get; set; }

    /// <summary>
    /// 如果 code 是 null，则默认返回 message 信息
    /// 如果 code 有值，则会根据 code 走本地化方法并最终返回本地化消息
    /// </summary>
    /// <param name="code">异常代码</param>
    /// <param name="message">异常消息</param>
    /// <param name="data"></param>
    public EcisBusinessException(string code = "", string message = "", object data = default) : base(code, message)
    {
        this.Data = data;
    }
}