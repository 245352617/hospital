using YiJian.Handover.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.Handover
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(HandoverEntityFrameworkCoreTestModule)
        )]
    public class HandoverDomainTestModule : AbpModule
    {
        
    }
}
