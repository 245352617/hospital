using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.EMR.EntityFrameworkCore
{
    public class EMRModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public EMRModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "Emr",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}