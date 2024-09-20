using Localization.Resources.AbpUi;
using YiJian.ECIS.IMService.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class IMServiceHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(IMServiceHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<IMServiceResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
