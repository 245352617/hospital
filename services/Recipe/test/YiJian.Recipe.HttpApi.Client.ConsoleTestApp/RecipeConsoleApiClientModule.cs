using Volo.Abp.Http.Client.IdentityModel;
using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeHttpApiClientModule),
        typeof(AbpHttpClientIdentityModelModule)
        )]
    public class RecipeConsoleApiClientModule : AbpModule
    {
        
    }
}
