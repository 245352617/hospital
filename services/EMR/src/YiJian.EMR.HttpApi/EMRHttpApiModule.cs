using Localization.Resources.AbpUi;
using YiJian.EMR.Localization;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Microsoft.Extensions.DependencyInjection;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class EMRHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(EMRHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<EMRResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
