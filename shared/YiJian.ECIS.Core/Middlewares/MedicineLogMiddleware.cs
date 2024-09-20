using Microsoft.AspNetCore.Http;
using Serilog.Context;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using YiJian.ECIS.Core.Enrichers;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
/// 日志中间件
/// </summary>
public class MedicineLogMiddleware : IMiddleware, ITransientDependency
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    /// <param name="next"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var sp = context.RequestServices;
        using (LogContext.Push(new ServiceNameEnricher(sp)))
        {
            await next(context);
        }
    }
}