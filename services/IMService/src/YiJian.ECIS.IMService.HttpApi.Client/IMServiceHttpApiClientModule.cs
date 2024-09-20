using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(IMServiceApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class IMServiceHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "IMService";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(IMServiceApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
