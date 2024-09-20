using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Rp
{
    /// <summary>
    /// 抢救室滞留时间中位数季度比
    /// </summary>
    public class RpQuarterEmergencyroomAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsQuarterEmergencyroomAndPatient, int>, IRpQuarterEmergencyroomAndPatientRepository
    {
        /// <summary>
        /// 抢救室滞留时间中位数季度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpQuarterEmergencyroomAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
         
        /// <summary>
        /// 抢救室滞留时间中位数季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsQuarterEmergencyroomAndPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.YearQuarter >= begin && w.YearQuarter <= end);
            return await query.ToListAsync();
        }
    }
}
