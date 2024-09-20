using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Health.Report.EntityFrameworkCore
{
    [ConnectionStringName(ReportDbProperties.ConnectionStringName)]
    public interface IReportDbContext : IEfCoreDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * DbSet<Question> Questions { get; }
         */
    }
}