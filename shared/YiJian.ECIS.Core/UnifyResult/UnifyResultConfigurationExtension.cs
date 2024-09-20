using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Net;
using System.Reflection;
using Volo.Abp.AspNetCore.Mvc.ApiExploring;
using Volo.Abp.AspNetCore.Mvc.ExceptionHandling;
using Volo.Abp.Modularity;
using YiJian.ECIS.Core.DynamicApiController;
using YiJian.ECIS.Core.Middlewares;
using YiJian.ECIS.Core.UnifyResult;

namespace YiJian.ECIS.Core;

/// <summary>
/// 统一返回配置
/// </summary>
public static class UnifyResultConfigurationExtension
{
    /// <summary>
    /// 配置统一返回
    /// </summary>
    /// <param name="context"></param>
    public static void AddUnifyResult<TUnifyResultProvider>(
       this ServiceConfigurationContext context)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        context.Services.Configure<MvcOptions>(options =>
        {
            // 替换 ABP 原有的全局异常处理
            options.Filters.ReplaceOne(x => (x as ServiceFilterAttribute)?.ServiceType?.Name == nameof(AbpExceptionFilter), new ServiceFilterAttribute(typeof(ExceptionUnifyFilter)));
            // 添加自定义的全局统一返回处理
            options.Filters.AddService(typeof(SucceededUnifyResultFilter));
            // 添加应用模型转换器
            options.Conventions.Add(new DynamicApiControllerApplicationModelConvention());
        });
        context.Services.Configure<AbpRemoteServiceApiDescriptionProviderOptions>(options =>
        {
            // 移除 ABP 预设的 API 返回描述（关于错误）
            options.SupportedResponseTypes.RemoveWhere(
                x => x.StatusCode == (int)HttpStatusCode.Forbidden
                || x.StatusCode == (int)HttpStatusCode.Unauthorized
                || x.StatusCode == (int)HttpStatusCode.BadRequest
                || x.StatusCode == (int)HttpStatusCode.NotFound
                || x.StatusCode == (int)HttpStatusCode.NotImplemented
                || x.StatusCode == (int)HttpStatusCode.InternalServerError);
        });

        // 添加规范化提供器
        context.Services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();
        // 获取规范化提供器模型
        UnifyContext.RESTfulResultType = typeof(TUnifyResultProvider).GetCustomAttribute<UnifyModelAttribute>().ModelType;
    }

    /// <summary>
    /// 配置统一返回(只处理异常的统一返回)
    /// </summary>
    /// <param name="context"></param>
    public static void AddErrorUnifyResult<TUnifyResultProvider>(
       this ServiceConfigurationContext context)
        where TUnifyResultProvider : class, IUnifyResultProvider
    {
        context.Services.Configure<MvcOptions>(options =>
        {
            // 替换 ABP 原有的全局异常处理
            options.Filters.ReplaceOne(x => (x as ServiceFilterAttribute)?.ServiceType?.Name == nameof(AbpExceptionFilter), new ServiceFilterAttribute(typeof(ExceptionUnifyFilter)));
        });
        context.Services.Configure<AbpRemoteServiceApiDescriptionProviderOptions>(options =>
        {
            // 移除 ABP 预设的 API 返回描述（关于错误）
            options.SupportedResponseTypes.RemoveWhere(
                x => x.StatusCode == (int)HttpStatusCode.Forbidden
                || x.StatusCode == (int)HttpStatusCode.Unauthorized
                || x.StatusCode == (int)HttpStatusCode.BadRequest
                || x.StatusCode == (int)HttpStatusCode.NotFound
                || x.StatusCode == (int)HttpStatusCode.NotImplemented
                || x.StatusCode == (int)HttpStatusCode.InternalServerError);
        });

        // 添加规范化提供器
        context.Services.AddSingleton<IUnifyResultProvider, TUnifyResultProvider>();
        // 获取规范化提供器模型
        UnifyContext.RESTfulResultType = typeof(TUnifyResultProvider).GetCustomAttribute<UnifyModelAttribute>().ModelType;
    }

}
