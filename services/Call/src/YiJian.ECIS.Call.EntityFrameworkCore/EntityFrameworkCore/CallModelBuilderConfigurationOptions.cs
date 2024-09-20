using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    public class CallModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public CallModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}