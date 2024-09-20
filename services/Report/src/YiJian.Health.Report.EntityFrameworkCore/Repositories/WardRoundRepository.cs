using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 查房信息
    /// </summary>
    public class WardRoundRepository : EfCoreRepository<ReportDbContext, WardRound, Guid>, IWardRoundRepository
    {
        /// <summary>
        /// 查房信息
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public WardRoundRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
    }
}
