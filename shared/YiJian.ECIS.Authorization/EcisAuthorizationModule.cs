using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Modularity;
using Volo.Abp.Users;

namespace YiJian.ECIS.Authorization;

/// <summary>
/// 急危重症一体化平台认证相关功能模块
/// </summary>
public class EcisAuthorizationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // 覆盖 Abp 的当前用户定义，与急危重症一体化平台保持一致，不会因 Abp 版本更新导致问题
        context.Services.AddSingleton<ICurrentUser, EcisCurrentUser>();
    }
}