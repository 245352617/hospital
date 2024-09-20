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
    /// 急诊抢救室患者死亡率季度比
    /// </summary>
    public class RpQuarterEmergencyroomAndDeathPatientRepository : EfCoreRepository<ReportDbContext, StatisticsQuarterEmergencyroomAndDeathPatient, int>, IRpQuarterEmergencyroomAndDeathPatientRepository
    {
        /// <summary>
        /// 急诊抢救室患者死亡率季度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpQuarterEmergencyroomAndDeathPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 急诊抢救室患者死亡率季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsQuarterEmergencyroomAndDeathPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.YearQuarter >= begin && w.YearQuarter <= end);
            return await query.ToListAsync();
        }
    }
}
