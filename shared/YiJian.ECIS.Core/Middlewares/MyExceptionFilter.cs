using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.ExceptionHandling;
using Volo.Abp.DependencyInjection;
using Volo.Abp.ExceptionHandling;
using Volo.Abp.Http;
using Volo.Abp.Json;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.ECIS.Core.Middlewares;

/// <summary>
/// 自定义异常过滤器
/// </summary>
public class MyExceptionFilter : IAsyncExceptionFilter, ITransientDependency
{
    public ILogger<MyExceptionFilter> Logger { get; set; }

    private readonly IExceptionToErrorInfoConverter _errorInfoConverter;
    private readonly IHttpExceptionStatusCodeFinder _statusCodeFinder;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly AbpExceptionHandlingOptions _exceptionHandlingOptions;

    public MyExceptionFilter(
        IExceptionToErrorInfoConverter errorInfoConverter,
        IHttpExceptionStatusCodeFinder statusCodeFinder,
        IJsonSerializer jsonSerializer,
        IOptions<AbpExceptionHandlingOptions> exceptionHandlingOptions)
    {
        _errorInfoConverter = errorInfoConverter;
        _statusCodeFinder = statusCodeFinder;
        _jsonSerializer = jsonSerializer;
        _exceptionHandlingOptions = exceptionHandlingOptions.Value;

        Logger = NullLogger<MyExceptionFilter>.Instance;
    }

    public async Task OnExceptionAsync(ExceptionContext context)
    {
        if (!ShouldHandleException(context))
        {
            return;
        }

        await HandleAndWrapExceptionAsync(context);
    }

    protected virtual bool ShouldHandleException(ExceptionContext context)
    {
        if (context.ActionDescriptor.IsControllerAction()
                // &&
                // context.ActionDescriptor.HasObjectResult()
                )
        {
            return true;
        }

        if (context.HttpContext.Request.CanAccept(MimeTypes.Application.Json))
        {
            return true;
        }

        if (context.HttpContext.Request.IsAjax())
        {
            return true;
        }

        return false;
    }

    protected virtual async Task HandleAndWrapExceptionAsync(ExceptionContext context)
    {
        context.HttpContext.Response.Headers.Add(AbpHttpConsts.AbpErrorFormat, "true");
        //context.HttpContext.Response.StatusCode = (int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception);

        context.HttpContext.Response.StatusCode = StatusCodes.Status200OK;

        try
        {
            //指定要业务的code
            var statusCode = (EStatusCode)((int)_statusCodeFinder.GetStatusCode(context.HttpContext, context.Exception));
            context.Result = new JsonResult(new ResponseBase<bool>(statusCode, false, context.Exception.Message));
        }
        catch
        {
            context.Result = new JsonResult(new ResponseBase<bool>(EStatusCode.C500, false, context.Exception.Message));
        }

        // 写日志
        var remoteServiceErrorInfo = _errorInfoConverter.Convert(context.Exception, _exceptionHandlingOptions.SendExceptionsDetailsToClients);
        remoteServiceErrorInfo.Code = context.HttpContext.TraceIdentifier;
        remoteServiceErrorInfo.Message = context.Exception.Message;
        var logLevel = context.Exception.GetLogLevel();
        var remoteServiceErrorInfoBuilder = new StringBuilder();
        remoteServiceErrorInfoBuilder.AppendLine($"---------- {nameof(RemoteServiceErrorInfo)} ----------");
        remoteServiceErrorInfoBuilder.AppendLine(_jsonSerializer.Serialize(remoteServiceErrorInfo, indented: true));
        Logger.LogWithLevel(logLevel, remoteServiceErrorInfoBuilder.ToString());
        Logger.LogException(context.Exception, logLevel);

        await context.HttpContext
            .RequestServices
            .GetRequiredService<IExceptionNotifier>()
            .NotifyAsync(
                new ExceptionNotificationContext(context.Exception)
            );

        context.Exception = null; //Handled!
    }

}
