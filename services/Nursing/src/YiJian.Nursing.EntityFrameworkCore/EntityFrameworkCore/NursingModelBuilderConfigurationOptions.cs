using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.Nursing.EntityFrameworkCore
{
    public class NursingModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public NursingModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}