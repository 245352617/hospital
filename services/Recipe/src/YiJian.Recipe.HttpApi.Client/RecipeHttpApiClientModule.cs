using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Http.Client;
using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeApplicationContractsModule),
        typeof(AbpHttpClientModule))]
    public class RecipeHttpApiClientModule : AbpModule
    {
        public const string RemoteServiceName = "Recipe";

        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            context.Services.AddHttpClientProxies(
                typeof(RecipeApplicationContractsModule).Assembly,
                RemoteServiceName
            );
        }
    }
}
