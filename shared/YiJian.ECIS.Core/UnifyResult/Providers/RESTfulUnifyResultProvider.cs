using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Http;
using YiJian.ECIS.Core.UnifyResult;
using YiJian.ECIS.ShareModel.Exceptions;

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 统一返回提供器
/// </summary>
[UnifyModel(typeof(RESTfulResult<>))]
public class RESTfulUnifyResultProvider : IUnifyResultProvider
{
    /// <summary>
    /// 规范化结果类型
    /// </summary>
    public static Type RESTfulResultType { get; set; } = typeof(RESTfulResult<>);

    /// <summary>
    /// 拦截返回状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    public async Task OnResponseStatusCodesAsync(HttpContext context, int statusCode)
    {
        // 设置返回的状态码为 200
        context.Response.StatusCode = StatusCodes.Status200OK;

        switch (statusCode)
        {
            // 处理 401 状态码
            case StatusCodes.Status401Unauthorized:
                await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: "401 Unauthorized"));
                break;
            // 处理 403 状态码
            case StatusCodes.Status403Forbidden:
                await context.Response.WriteAsJsonAsync(RESTfulResult(statusCode, errors: "403 Forbidden"));
                break;

            default: break;
        }
    }

    /// <summary>
    /// 成功返回处理
    /// </summary>
    /// <param name="context"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    public IActionResult OnSucceeded(ActionExecutedContext context, object data)
    {
        // 使用 OkObjectResult 而不使用 JsonResult 可以防止接口返回 204 No Cotent
        // 所有 204 的返回都会变成 200 
        return new OkObjectResult(RESTfulResult(StatusCodes.Status200OK, success: true, data: data));
    }

    /// <summary>
    /// 返回 RESTful 风格结果集
    /// </summary>
    /// <param name="statusCode"></param>
    /// <param name="success"></param>
    /// <param name="message"></param>
    /// <param name="data"></param>
    /// <param name="errors"></param>
    /// <returns></returns>
    private static RESTfulResult<object> RESTfulResult(int statusCode, bool success = default, string message = "",
        object data = default, object errors = default)
    {
        return new RESTfulResult<object>
        {
            Code = statusCode,
            Success = success,
            Message = !string.IsNullOrEmpty(message) ? message : (success ? "操作成功" : "操作失败"),
            Data = data,
            Errors = errors,
            Timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
        };
    }

    /// <summary>
    /// 异常返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="remoteServiceErrorInfo"></param>
    /// <returns></returns>
    public IActionResult OnException(ExceptionContext context, RemoteServiceErrorInfo remoteServiceErrorInfo)
    {
        // 该方法只处理 状态码 >= 400 的错误
        // 获取 http 状态码
        var httpCode = (int)context
            .GetRequiredService<IHttpExceptionStatusCodeFinder>()
            .GetStatusCode(context.HttpContext, context.Exception);
        if (httpCode == 400)
        {
            // Bad Request 特殊处理
            return new JsonResult(RESTfulResult(httpCode, success: false, message: remoteServiceErrorInfo.Message,
                errors: remoteServiceErrorInfo.ValidationErrors));
        }

        // 友好异常返回（返回code: 403）
        if (context.Exception.GetType() == typeof(EcisBusinessException))
        {
            var data = (context.Exception as EcisBusinessException).Data;
            return new OkObjectResult(RESTfulResult(httpCode, success: false,
                message: remoteServiceErrorInfo.Message, data));
        }

        // 友好异常返回（返回code: 403）
        if (context.Exception.GetType().GetInterfaces().Contains(typeof(IUserFriendlyException)))
        {
            return new OkObjectResult(RESTfulResult(httpCode, success: false,
                message: remoteServiceErrorInfo.Message));
        }

        // 普通异常返回（返回code: 500）
        if (context.Exception.GetType() == typeof(Exception))
        {
            return new OkObjectResult(RESTfulResult(httpCode, success: false, message: context.Exception.Message));
        }

        // 其他未处理的错误信息统一返回
        return new OkObjectResult(RESTfulResult(httpCode, success: false, message: context.Exception.Message,
            errors: context.Exception.Message));
    }
}