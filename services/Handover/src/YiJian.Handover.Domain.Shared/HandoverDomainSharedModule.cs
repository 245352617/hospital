using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using YiJian.Handover.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;

namespace YiJian.Handover
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class HandoverDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<HandoverDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<HandoverResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/Handover")
                    .AddVirtualJson("/Localization/Handover/Exception");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("Handover", typeof(HandoverResource));
            });
        }
    }
}
