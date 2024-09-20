using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using YiJian.EMR.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.ShareModel;

namespace YiJian.EMR
{
    [DependsOn(
        typeof(ShareModelModule),
        typeof(AbpValidationModule)
    )]
    public class EMRDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<EMRDomainSharedModule>();
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                options.Resources
                    .Add<EMRResource>("en")
                    .AddBaseTypes(typeof(AbpValidationResource))
                    .AddVirtualJson("/Localization/EMR");
            });

            Configure<AbpExceptionLocalizationOptions>(options =>
            {
                options.MapCodeNamespace("EMR", typeof(EMRResource));
            });
        }
    }
}
