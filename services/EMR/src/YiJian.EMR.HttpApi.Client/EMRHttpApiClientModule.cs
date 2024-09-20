using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(EMRApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class EMRHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "EMR";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(EMRApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
