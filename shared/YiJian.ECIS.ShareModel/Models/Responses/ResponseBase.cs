using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.ECIS.ShareModel.Responses;

/// <summary>
/// 返回的基类
/// </summary>
public class ResponseBase<T>
{
    /// <summary>
    /// 默认操作,默认返回OK [1]
    /// </summary>
    public ResponseBase()
    {
        this.Code = EStatusCode.COK;
        this.Message = this.Code.GetDescription();
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="code">状态</param>
    public ResponseBase(EStatusCode code)
    {
        this.Code = code;
        this.Message = this.Code.GetDescription();
        this.Data = default(T);
    }

    /// <summary>
    /// 返回 [自定义返回提示]
    /// </summary>
    /// <param name="code">状态</param>
    /// <param name="message">数据</param>
    public ResponseBase(EStatusCode code, string message)
    {
        this.Code = code;
        this.Message = message;
        this.Data = default(T);
    }

    public ResponseBase(EStatusCode code, T data, int totalCount)
    {
        this.Code = code;
        this.Message = this.Code.GetDescription();
        this.Data = data;
        this.TotalCount = totalCount;
    }

    /// <summary>
    /// 返回
    /// </summary>
    /// <param name="code">状态</param>
    /// <param name="data">数据</param>
    public ResponseBase(EStatusCode code, T data)
    {
        this.Code = code;
        this.Message = this.Code.GetDescription();
        this.Data = data;
    }

    /// <summary>
    /// 自定义描述返回
    /// </summary>
    /// <param name="code">状态</param>
    /// <param name="data">数据</param>
    /// <param name="message">描述</param>
    public ResponseBase(EStatusCode code, T data, string message)
    {
        this.Code = code;
        this.Message = message;
        this.Data = data;
    }

    /// <summary>
    /// 自定义描述返回
    /// </summary>
    /// <param name="code">状态</param>
    /// <param name="data">数据</param>
    /// <param name="exten">扩展描述</param>
    public ResponseBase(EStatusCode code, T data, Dictionary<string, object> exten)
    {
        this.Code = code;
        this.Message = this.Code.GetDescription();
        this.Data = data;
        this.Exten = exten;
    }

    /// <summary>
    /// 设置Data
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static ResponseBase<T> SetData(T t)
    {
        ResponseBase<T> responseBase = new ResponseBase<T>();
        responseBase.Data = t;
        return responseBase;
    }

    /// <summary>
    /// 返回状态码
    /// </summary>
    public EStatusCode Code { get; set; }

    /// <summary>
    /// 返回的消息提示
    /// </summary>
    public string Message { get; set; }

    /// <summary>
    /// 返回的数据
    /// </summary>
    public T Data { get; set; }


    /// <summary>
    /// 总记录数
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// 返回的分页信息
    /// </summary>
    public PageResulModel? Page { get; set; }

    /// <summary>
    /// 扩展字段返回类型，以字典模式返回，主要是承接错误或一些特殊的数据
    /// </summary> 
    public Dictionary<string, object> Exten { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// 追加扩展描述
    /// </summary> 
    /// <param name="key"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public ResponseBase<T> AppendExten(string key, object value)
    {
        if (key.IsNullOrEmpty())
        {
            return this;
        }

        if (!this.Exten.ContainsKey(key))
        {
            this.Exten.Add(key, value);
        }
        return this;
    }

    /// <summary>
    /// 
    /// </summary> 
    /// <param name="exten"></param>
    /// <returns></returns> 
    public ResponseBase<T> AppendExten(Dictionary<string, object> exten)
    {
        if (exten.Count == 0)
        {
            return this;
        }

        foreach (var item in exten)
        {
            if (!this.Exten.ContainsKey(item.Key))
            {
                this.Exten.Add(item.Key, item.Value);
            }
        }
        return this;
    }

    /// <summary>
    /// 强制修改状态
    /// </summary>
    /// <param name="code"></param>
    public void SetCode(EStatusCode code)
    {
        Code = code;
        Message = code.GetDescription();
    }

}
