using Volo.Abp.Domain;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace YiJian.Nursing
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(NursingDomainSharedModule),
        typeof(AbpEventBusRabbitMqModule)
    )]
    public class NursingDomainModule : AbpModule
    {

    }
}
