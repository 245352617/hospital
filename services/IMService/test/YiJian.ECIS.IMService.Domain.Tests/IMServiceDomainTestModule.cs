using YiJian.ECIS.IMService.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.ECIS.IMService
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(IMServiceEntityFrameworkCoreTestModule)
        )]
    public class IMServiceDomainTestModule : AbpModule
    {
        
    }
}
