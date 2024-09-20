using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Volo.Abp.Validation;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
/// 异常处理中间件
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        this.next = next;
        this._logger = logger;
    }

    /// <summary>
    /// Invoke
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (AbpValidationException ex)
        {
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    /// <summary>
    /// 异常处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="ex"></param>
    /// <returns></returns>
    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        var statusCode = context.Response.StatusCode;
        var desc = ((EStatusCode)statusCode).GetDescription();
        string ip = context.Connection.RemoteIpAddress.ToString();

        string msg = $"[err] ip:[{ip}] 异常描述:{ex.Message} , 堆栈信息:{ex.StackTrace} ";

        if (ex.GetType() == typeof(BusException))
        {
            //如果是自定义异常，则不做处理
            _logger.LogError(msg, ex);
            return;
        }

        var res = new ResponseBase<string>((EStatusCode)statusCode, ex.Message);
        var err = JsonConvert.SerializeObject(res);
        await context.Response.WriteAsync(err);
    }
}