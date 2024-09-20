using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.PrintSettings;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 打印设置
    /// </summary>
    public class PrintSettingRepository : EfCoreRepository<ReportDbContext, PrintSetting, Guid>, IPrintSettingRepository
    {
        public PrintSettingRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}