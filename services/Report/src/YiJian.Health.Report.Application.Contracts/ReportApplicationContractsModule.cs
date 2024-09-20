using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.Authorization;

namespace YiJian.Health.Report
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(ReportDomainSharedModule),
        typeof(AbpDddApplicationContractsModule),
        typeof(AbpAuthorizationModule)
        )]
    public class ReportApplicationContractsModule : AbpModule
    {

    }
}
