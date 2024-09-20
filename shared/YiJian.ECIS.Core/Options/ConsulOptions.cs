namespace YiJian.ECIS.Core.Options;

/// <summary>
///     服务发现配置
/// </summary>
public class ConsulOptions
{
    /// <summary>
    /// 服务器名称
    /// </summary>
    public static string Name = "Consul";

    /// <summary>
    ///     是否启用服务发现
    /// </summary>
    public bool IsEnabled { get; set; } = false;

    /// <summary>
    ///     Consul服务地址
    /// </summary>
    public string ConsulUrl { get; set; } = string.Empty;

    /// <summary>
    ///     服务地址
    /// </summary>
    public string ServiceUrl { get; set; } = string.Empty;

    /// <summary>
    ///     服务名称
    /// </summary>
    public string ServiceName { get; set; } = string.Empty;
}