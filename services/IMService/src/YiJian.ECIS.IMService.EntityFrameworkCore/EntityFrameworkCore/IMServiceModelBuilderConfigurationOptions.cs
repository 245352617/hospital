using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    public class IMServiceModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public IMServiceModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}