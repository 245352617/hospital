using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(HandoverDomainSharedModule),
		typeof(AbpEventBusRabbitMqModule))]
    public class HandoverDomainModule : AbpModule
    {

    }
}
