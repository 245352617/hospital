using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.Call
{
    [DependsOn(
        typeof(CallApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class CallHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Call";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(CallApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
