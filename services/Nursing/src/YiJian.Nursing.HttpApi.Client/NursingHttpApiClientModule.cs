using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    [DependsOn(
        typeof(NursingApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class NursingHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Nursing";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(NursingApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
