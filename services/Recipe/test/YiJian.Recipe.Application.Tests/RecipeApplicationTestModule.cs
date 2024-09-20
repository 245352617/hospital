using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(RecipeApplicationModule),
        typeof(RecipeDomainTestModule)
        )]
    public class RecipeApplicationTestModule : AbpModule
    {

    }
}
