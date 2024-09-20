using Localization.Resources.AbpUi;
using YiJian.Handover.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(HandoverApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class HandoverHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(HandoverHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<HandoverResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
