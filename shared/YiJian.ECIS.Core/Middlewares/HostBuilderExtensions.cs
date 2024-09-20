using Microsoft.Extensions.Hosting;
using System.Runtime.InteropServices;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
///  主机构造器扩展
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// UseAsDaemon
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IHostBuilder UseAsDaemon(this IHostBuilder builder)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return builder.UseWindowsService();

        return RuntimeInformation.IsOSPlatform(OSPlatform.Linux) ? builder.UseSystemd() : builder;
    }
}
