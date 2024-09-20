using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    public class ReportModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ReportModelBuilderConfigurationOptions(
            [NotNull] string tablePrefix = "Rp",
            [CanBeNull] string schema = null)
            : base(
                tablePrefix,
                schema)
        {

        }
    }
}