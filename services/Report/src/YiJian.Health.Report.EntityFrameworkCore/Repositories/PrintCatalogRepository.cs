using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.PrintCatalogs;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 打印目录
    /// </summary>
    public class PrintCatalogRepository : EfCoreRepository<ReportDbContext, PrintCatalog, Guid>, IPrintCatalogRepository
    {
        public PrintCatalogRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}