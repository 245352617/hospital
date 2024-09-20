using YiJian.Nursing.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(NursingEntityFrameworkCoreTestModule)
        )]
    public class NursingDomainTestModule : AbpModule
    {
        
    }
}
