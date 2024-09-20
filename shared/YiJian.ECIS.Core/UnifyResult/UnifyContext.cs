using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Reflection;
using YiJian.ECIS.Core.Extensions;

namespace YiJian.ECIS.Core.UnifyResult;

/// <summary>
/// 规范化结果上下文
/// </summary>
public static class UnifyContext
{
    /// <summary>
    /// 规范化结果类型
    /// </summary>
    internal static Type RESTfulResultType = typeof(RESTfulResult<>);

    internal static IServiceProvider ServiceProvider;

    /// <summary>
    /// 检查请求成功是否进行规范化处理
    /// </summary>
    /// <param name="method"></param>
    /// <param name="unifyResult"></param>
    /// <param name="isWebRequest"></param>
    /// <returns>返回 true 跳过处理，否则进行规范化处理</returns>
    internal static bool CheckSucceededNonUnify(MethodInfo method, out IUnifyResultProvider unifyResult, bool isWebRequest = true)
    {
        // 判断是否跳过规范化处理
        var isSkip = method.GetRealReturnType().HasImplementedRawGeneric(RESTfulResultType)
              || method.CustomAttributes.Any(x => typeof(NonUnifyAttribute).IsAssignableFrom(x.AttributeType)
              || typeof(ProducesResponseTypeAttribute).IsAssignableFrom(x.AttributeType)
              || typeof(IApiResponseMetadataProvider).IsAssignableFrom(x.AttributeType))
              || method.ReflectedType.IsDefined(typeof(NonUnifyAttribute), true);

        if (!isWebRequest)
        {
            unifyResult = null;
            return isSkip;
        }

        unifyResult = isSkip ? null : ServiceProvider.GetService<IUnifyResultProvider>();
        return unifyResult == null || isSkip;
    }
}
