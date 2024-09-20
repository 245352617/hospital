using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class RecipeApplicationContractsModule : AbpModule
    {

    }
}
