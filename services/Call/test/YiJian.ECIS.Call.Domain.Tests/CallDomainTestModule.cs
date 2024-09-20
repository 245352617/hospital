using YiJian.ECIS.Call.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.Call
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(CallEntityFrameworkCoreTestModule)
        )]
    public class CallDomainTestModule : AbpModule
    {
        
    }
}
