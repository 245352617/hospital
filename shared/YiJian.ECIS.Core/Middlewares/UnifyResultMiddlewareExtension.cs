using Microsoft.AspNetCore.Builder;
using YiJian.ECIS.Core.UnifyResult;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
/// 统一返回中间件扩展方法
/// </summary>
public static class UnifyResultMiddlewareExtension
{
    /// <summary>
    /// 添加状态码拦截中间件
    /// </summary>
    /// <param name="builder"></param>
    /// <returns></returns>
    public static IApplicationBuilder UseUnifyResultStatusCodes(this IApplicationBuilder builder)
    {
        // 注册中间件
        builder.UseMiddleware<UnifyResultStatusCodesMiddleware>();
        UnifyContext.ServiceProvider = builder.ApplicationServices;

        return builder;
    }
}
