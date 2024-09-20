using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;
using YiJian.ECIS.ShareModel;
using YiJian.ECIS.ShareModel.DDPs;

namespace YiJian.ECIS.DDP;

/// <summary>
/// Ddp模块
/// </summary> 
[DependsOn(
    typeof(ShareModelModule),
    typeof(AbpAutofacModule)
)]
public class DdpModule : AbpModule
{
    /// <inheritdoc/>
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        context.Services.Configure<DdpHospital>(configuration.GetSection("DdpHospital"));
        context.Services.AddSingleton(typeof(DdpHospital));
        context.Services.AddSingleton(typeof(DdpSwitch));
    }

}
