using Volo.Abp.Domain;
using Volo.Abp.EventBus.RabbitMq;
using Volo.Abp.Modularity;

namespace YiJian.Recipe
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(RecipeDomainSharedModule),
        typeof(AbpEventBusRabbitMqModule))]
    public class RecipeDomainModule : AbpModule
    {

    }
}
