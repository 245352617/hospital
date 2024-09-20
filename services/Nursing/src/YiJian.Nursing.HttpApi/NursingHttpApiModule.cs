using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    [DependsOn(
        typeof(NursingApplicationContractsModule),
        typeof(AbpAspNetCoreMvcModule))]
    public class NursingHttpApiModule : AbpModule
    {
        public override void PreConfigureServices(ServiceConfigurationContext context)
        {
            PreConfigure<IMvcBuilder>(mvcBuilder =>
            {
                mvcBuilder.AddApplicationPartIfNotExists(typeof(NursingHttpApiModule).Assembly);
            });
        }
    }
}
