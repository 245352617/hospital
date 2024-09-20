using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments;
using YiJian.Health.Report.NursingDocuments.Contracts;
using YiJian.Health.Report.NursingDocuments.Entities;

namespace YiJian.Health.Report.Repositories
{

    public class IntakeStatisticsRepository : EfCoreRepository<ReportDbContext, IntakeStatistics, Guid>, IIntakeStatisticsRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public IntakeStatisticsRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
        }
        /// <summary>
        /// 查询出入量统计信息
        /// </summary>
        /// <param name="nursingDocumentId"></param>
        /// <param name="sheetIndex"></param>
        /// <param name="begintime"></param>
        /// <param name="endtime"></param>
        /// <returns></returns>
        public async Task<List<IntakeStatistics>> GetIntakeStatisticsListAsync(Guid nursingDocumentId, DateTime? begintime, DateTime? endtime, int sheetIndex = 0)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.Where(w => w.NursingDocumentId == nursingDocumentId && w.SheetIndex == sheetIndex)
                .WhereIf(begintime.HasValue, w => w.Begintime >= begintime)
                .WhereIf(endtime.HasValue, w => w.Endtime <= endtime)
                .OrderBy(o => o.Endtime)
                .ToListAsync();
            return query;
        }
    }
}
