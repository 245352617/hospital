using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System;
using System.Linq;
using YiJian.ECIS.Core.Extensions;
using YiJian.ECIS.Core.UnifyResult;

namespace YiJian.ECIS.Core.DynamicApiController;

/// <summary>
/// 动态接口控制器应用模型转换器
/// </summary>
public sealed class DynamicApiControllerApplicationModelConvention : IApplicationModelConvention
{
    ///// <summary>
    ///// 动态接口控制器配置实例
    ///// </summary>
    //private readonly DynamicApiControllerSettingsOptions _dynamicApiControllerSettings;

    /// <summary>
    /// 构造函数
    /// </summary>
    public DynamicApiControllerApplicationModelConvention()
    {
        //_dynamicApiControllerSettings = App.GetConfig<DynamicApiControllerSettingsOptions>("DynamicApiControllerSettings", true);
    }

    /// <summary>
    /// 配置应用模型信息
    /// </summary>
    /// <param name="application">引用模型</param>
    public void Apply(ApplicationModel application)
    {
        var controllers = application.Controllers.Where(u => Penetrates.IsApiController(u.ControllerType));
        foreach (var controller in controllers)
        {
            var controllerType = controller.ControllerType;

            // 判断是否处理 Mvc控制器
            if (typeof(ControllerBase).IsAssignableFrom(controller.ControllerType))
            {
                //if (!_dynamicApiControllerSettings.SupportedMvcController.Value || controller.ApiExplorer?.IsVisible == false) continue;
            }

            ConfigureController(controller);
        }
    }

    /// <summary>
    /// 配置控制器
    /// </summary>
    /// <param name="controller">控制器模型</param>
    private void ConfigureController(ControllerModel controller)
    {
        var actions = controller.Actions;

        // 查找所有重复的方法签名
        var repeats = actions.GroupBy(u => new { u.ActionMethod.ReflectedType.Name, Signature = u.ActionMethod.ToString() })
                             .Where(u => u.Count() > 1)
                             .SelectMany(u => u.Where(u => u.ActionMethod.ReflectedType.Name != u.ActionMethod.DeclaringType.Name));

        // 2021年04月01日 https://docs.microsoft.com/en-US/aspnet/core/web-api/?view=aspnetcore-5.0#binding-source-parameter-inference
        // 判断是否贴有 [ApiController] 特性
        var hasApiControllerAttribute = controller.Attributes.Any(u => u.GetType() == typeof(ApiControllerAttribute));

        foreach (var action in actions)
        {
            // 跳过相同方法签名
            if (repeats.Contains(action))
            {
                action.ApiExplorer.IsVisible = false;
                continue;
            };

            var actionMethod = action.ActionMethod;
            //var actionApiDescriptionSettings = actionMethod.IsDefined(typeof(ApiDescriptionSettingsAttribute), true) ? actionMethod.GetCustomAttribute<ApiDescriptionSettingsAttribute>(true) : default;
            //ConfigureAction(action, actionApiDescriptionSettings, controllerApiDescriptionSettings, hasApiControllerAttribute);

            // 配置动作方法规范化特性
            //if (UnifyContext.EnabledUnifyHandler) 
            ConfigureActionUnifyResultAttribute(action);
        }
    }

    /// <summary>
    /// 配置规范化结果类型
    /// </summary>
    /// <param name="action"></param>
    private static void ConfigureActionUnifyResultAttribute(ActionModel action)
    {
        // 判断是否手动添加了标注或跳过规范化处理
        if (UnifyContext.CheckSucceededNonUnify(action.ActionMethod, out var _, false)) return;

        // 获取真实类型
        var returnType = action.ActionMethod.GetRealReturnType();
        {
            if (returnType == typeof(void))
            {
                // 统一处理无返回的规范化结果特性
                action.Filters.Add(new UnifyResultAttribute(typeof(object), StatusCodes.Status200OK));
            }
            else
            {
                // 添加规范化结果特性
                action.Filters.Add(new UnifyResultAttribute(returnType, StatusCodes.Status200OK));
            }
        }
    }
}