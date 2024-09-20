namespace YiJian.ECIS.ShareModel.Models.Responses;

/// <summary>
/// 对接平台统一返回包装层
/// </summary>
/// <typeparam name="T"></typeparam>
public class CommonResult<T>
{
    /// <summary>
    /// 默认构造函数
    /// </summary>
    public CommonResult()
    {
        Code = 0;
        Msg = "OK";
    }

    /// <summary>
    /// 默认成功的构造函数
    /// </summary>
    /// <param name="data"></param>
    public CommonResult(T data)
    {
        Code = 0;
        Msg = "OK";
        Data = data;
    }

    /// <summary>
    /// 完整的构造函数
    /// </summary>
    /// <param name="code"></param>
    /// <param name="msg"></param>
    /// <param name="data"></param>
    public CommonResult(int code, string msg, T data)
    {
        Code = code;
        Msg = msg;
        Data = data;
    }

    /// <summary>
    /// 状态码 , 0=成功，其他状态看消息提示
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 消息内容
    /// </summary>
    public string Msg { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 其他的返回类型
    /// </summary>
    public dynamic Message { get; set; }

}
