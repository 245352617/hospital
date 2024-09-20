namespace YiJian.ECIS.Core.Options;

/// <summary>
/// Consul Service Options
/// </summary>
public class ConsulServiceOptions
{
    /// <summary>
    /// 服务注册地址（Consul的地址）
    /// </summary>
    public string ConsulAddress { get; set; }
    /// <summary>
    /// 服务ID
    /// </summary>
    public string ServiceId { get; set; }
    /// <summary>
    /// 服务名称
    /// </summary>
    public string ServiceName { get; set; }
    /// <summary>
    /// 健康检查地址
    /// </summary>
    public string HealthCheck { get; set; }

}
