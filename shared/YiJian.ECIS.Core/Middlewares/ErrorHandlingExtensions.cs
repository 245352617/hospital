using Microsoft.AspNetCore.Builder;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
/// 异常处理中间件
/// </summary>
public static class ErrorHandlingExtensions
{
    /// <summary>
    /// 异常处理中间件
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseErrorHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<ErrorHandlingMiddleware>();
    }
}
