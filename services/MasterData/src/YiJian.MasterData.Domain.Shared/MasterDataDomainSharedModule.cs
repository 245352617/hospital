using Volo.Abp.Modularity;
using Volo.Abp.Localization;
using YiJian.MasterData.Localization;
using Volo.Abp.Localization.ExceptionHandling;
using Volo.Abp.Validation;
using Volo.Abp.Validation.Localization;
using Volo.Abp.VirtualFileSystem;
using YiJian.ECIS.ShareModel;

namespace YiJian.MasterData;

[DependsOn(
    typeof(AbpValidationModule),
    typeof(ShareModelModule)
)]
public class MasterDataDomainSharedModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpVirtualFileSystemOptions>(options =>
        {
            options.FileSets.AddEmbedded<MasterDataDomainSharedModule>();
        });

        Configure<AbpLocalizationOptions>(options =>
        {
            options.Resources
                .Add<MasterDataResource>("en")
                .AddBaseTypes(typeof(AbpValidationResource))
                .AddVirtualJson("/Localization/MasterData");
        });

        Configure<AbpExceptionLocalizationOptions>(options =>
        {
            options.MapCodeNamespace("MasterData", typeof(MasterDataResource));
        });
    }
}
