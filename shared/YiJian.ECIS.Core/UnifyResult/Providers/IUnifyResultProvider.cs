using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading.Tasks;
using Volo.Abp.Http;

namespace Microsoft.AspNetCore.Mvc;

/// <summary>
/// 统一返回提供器接口
/// </summary>
public interface IUnifyResultProvider
{
    /// <summary>
    /// 规范化结果类型
    /// </summary>
    public static Type RESTfulResultType { get; set; }

    /// <summary>
    /// 成功返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="data"></param>
    /// <returns></returns>
    IActionResult OnSucceeded(ActionExecutedContext context, object data);

    /// <summary>
    /// 异常返回值
    /// </summary>
    /// <param name="context"></param>
    /// <param name="remoteServiceErrorInfo"></param>
    /// <returns></returns>
    IActionResult OnException(ExceptionContext context, RemoteServiceErrorInfo remoteServiceErrorInfo);

    /// <summary>
    /// 拦截返回状态码
    /// </summary>
    /// <param name="context"></param>
    /// <param name="statusCode"></param>
    /// <returns></returns>
    Task OnResponseStatusCodesAsync(HttpContext context, int statusCode/*, UnifyResultSettingsOptions unifyResultSettings = default*/);
}
