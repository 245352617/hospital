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
    /// 急诊科各级患者比例季度比
    /// </summary>
    public class RpQuarterLevelAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsQuarterLevelAndPatient, int>, IRpQuarterLevelAndPatientRepository
    {
        /// <summary>
        /// 急诊科各级患者比例季度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpQuarterLevelAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
         
        /// <summary>
        /// 急诊科各级患者比例季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsQuarterLevelAndPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.YearQuarter >= begin && w.YearQuarter <= end);
            return await query.ToListAsync();
        }

    }
}
