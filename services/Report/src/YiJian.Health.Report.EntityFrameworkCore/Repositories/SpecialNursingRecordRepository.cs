using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 描述：特殊护理记录仓储类
    /// 创建人： yangkai
    /// 创建时间：2023/3/31 10:09:21
    /// </summary>
    public class SpecialNursingRecordRepository : EfCoreRepository<ReportDbContext, SpecialNursingRecord, Guid>, ISpecialNursingRecordRepository
    {
        public SpecialNursingRecordRepository(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
