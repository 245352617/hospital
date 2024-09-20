using Volo.Abp.Domain;
using Volo.Abp.Modularity;
using Volo.Abp.EventBus.RabbitMq;

namespace MyCompanyName.MyProjectName
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(MyProjectNameDomainSharedModule),
		typeof(AbpEventBusRabbitMqModule))]
    public class MyProjectNameDomainModule : AbpModule
    {

    }
}
