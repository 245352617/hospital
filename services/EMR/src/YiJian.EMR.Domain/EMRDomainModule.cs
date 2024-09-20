using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(EMRDomainSharedModule),
		typeof(AbpEventBusRabbitMqModule))]
    public class EMRDomainModule : AbpModule
    {

    }
}
