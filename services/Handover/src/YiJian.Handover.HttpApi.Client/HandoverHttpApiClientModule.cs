using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(HandoverApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class HandoverHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Handover";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(HandoverApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
