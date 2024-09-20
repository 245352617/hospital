using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs;

/// <summary>
/// DDP请求参数基类
/// </summary>
public class DdpBaseRequest<T>
{
    /// <summary>
    /// 构造函数(默认)
    /// </summary>
    public DdpBaseRequest()
    {

    }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="path"></param>
    /// <param name="req"></param>
    public DdpBaseRequest(string path, T req)
    {
        this.Path = path;
        this.Req = req;
    }

    /// <summary>
    /// 业务路由，需与业务调用规范定义path一致
    /// </summary>
    [JsonProperty("path")]
    public string Path { get; set; }

    /// <summary>
    /// 实际入参
    /// </summary>
    [JsonProperty("req")]
    public T Req { get; set; }

}
