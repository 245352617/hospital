using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using YiJian.ECIS.IMService.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace YiJian.ECIS.IMService
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class IMServiceDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<IMServiceDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<IMServiceResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/IMService")
                    .AddVirtualJson("/Localization/IMService/Exception");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("IMService", typeof(IMServiceResource));
            });
        }
    }
}
