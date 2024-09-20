using YiJian.Recipe.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(RecipeEntityFrameworkCoreTestModule)
        )]
    public class RecipeDomainTestModule : AbpModule
    {
        
    }
}
