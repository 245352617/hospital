namespace YiJian.Job.Models;

/// <summary>
/// 统一返回模型
/// </summary>
public class ResponseModel<T>
{
    public ResponseModel(int code, T data, string message)
    {
        Code = code;
        Data = data;
        Message = message;
    }

    public ResponseModel(T data)
    {
        Code = 200;
        Data = data;
        Message = "操作成功"; 
    }
     
    /// <summary>
    /// 状态
    /// </summary>
    public int Code { get; set; }

    /// <summary>
    /// 数据
    /// </summary>
    public T Data { get; set; }

    /// <summary>
    /// 消息
    /// </summary>
    public string Message { get; set; }

}
