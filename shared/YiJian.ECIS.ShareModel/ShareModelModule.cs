using Volo.Abp.Modularity;
using Volo.Abp.Validation;

namespace YiJian.ECIS.ShareModel;

/// <summary>
/// 
/// </summary>
[DependsOn(
   typeof(AbpValidationModule)
)]
public class ShareModelModule : AbpModule
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="context"></param>
    public override void PreConfigureServices(ServiceConfigurationContext context)
    {
        //context.Services.AddTransient(typeof(HttpClientUtil));
    }

}
