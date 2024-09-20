using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.ECIS.ShareModel.Exceptions;

/// <summary>
/// 自定义的业务异常
/// </summary>
public class BusException : Exception
{
    /// <summary>
    /// 
    /// </summary>
    public virtual EStatusCode Code { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public override string Message
    {
        get
        {
            if (Code != EStatusCode.CNormal)
            {
                return $"{base.Message} [{Code.GetDescription()}]";
            }
            return base.Message;
        }
    }

}
