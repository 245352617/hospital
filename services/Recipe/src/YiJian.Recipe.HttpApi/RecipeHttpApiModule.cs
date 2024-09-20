using Localization.Resources.AbpUi;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using YiJian.Recipe.Localization;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class RecipeHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(RecipeHttpApiModule).Assembly);
            });
        }

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Get<RecipeResource>()
                    .AddBaseTypes(typeof(AbpUiResource));
            });
        }
    }
}
