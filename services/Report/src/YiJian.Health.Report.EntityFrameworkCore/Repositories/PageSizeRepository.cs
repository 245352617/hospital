using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;

namespace YiJian.Health.Report.Repositories
{
    public class PageSizeRepository : EfCoreRepository<ReportDbContext, PageSize, Guid>, IPageSizeRepository
    {
        public PageSizeRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}