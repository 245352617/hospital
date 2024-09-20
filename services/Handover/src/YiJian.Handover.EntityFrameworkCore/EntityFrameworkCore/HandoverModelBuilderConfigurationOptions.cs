using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.Handover.EntityFrameworkCore
{
    public class HandoverModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public HandoverModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}