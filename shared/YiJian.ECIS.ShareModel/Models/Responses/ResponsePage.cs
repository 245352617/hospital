using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Responses;

/// <summary>
/// 带分页返回的对象
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResponsePage<T> : ResponseBase<T>
{
    /// <summary>
    /// 带分页返回的对象
    /// </summary>
    public ResponsePage()
    {
    }

    /// <summary>
    /// 带分页返回的对象
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <param name="count"></param>
    public ResponsePage(EStatusCode code, T data, int count) : base(code, data)
    {
        Count = count;
    }

    /// <summary>
    /// 带分页返回的对象
    /// </summary>
    /// <param name="code"></param>
    /// <param name="data"></param>
    /// <param name="message"></param>
    /// <param name="count"></param>
    public ResponsePage(EStatusCode code, T data, string message, int count) : base(code, data, message)
    {
        Count = count;
    }

    /// <summary>
    /// 总记录数
    /// </summary>
    public int Count { get; set; }

}
