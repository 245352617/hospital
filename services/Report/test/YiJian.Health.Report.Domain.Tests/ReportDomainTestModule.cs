using YiJian.Health.Report.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace YiJian.Health.Report
{
    /* Domain tests are configured to use the EF Core provider.
     * You can switch to MongoDB, however your domain tests should be
     * database independent anyway.
     */
    [DependsOn(
        typeof(ReportEntityFrameworkCoreTestModule)
        )]
    public class ReportDomainTestModule : AbpModule
    {
        
    }
}
