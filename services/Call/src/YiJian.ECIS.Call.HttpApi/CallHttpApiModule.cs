using Localization.Resources.AbpUi;
using YiJian.ECIS.Call.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(CallApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class CallHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(CallHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<CallResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
