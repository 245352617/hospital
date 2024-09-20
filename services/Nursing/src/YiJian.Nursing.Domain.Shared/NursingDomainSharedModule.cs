using Volo.Abp.Modularity;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace YiJian.Nursing
{
    [DependsOn(
        typeof(AbpValidationModule)
    )]
    public class NursingDomainSharedModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.AddEmbedded<NursingDomainSharedModule>();
            });
        }
    }
}
