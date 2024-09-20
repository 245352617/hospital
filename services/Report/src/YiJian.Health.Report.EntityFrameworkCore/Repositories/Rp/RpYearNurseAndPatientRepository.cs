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
    /// 急诊科护患年度比
    /// </summary>
    public class RpYearNurseAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsYearNurseAndPatient, int>, IRpYearNurseAndPatientRepository
    {
        /// <summary>
        /// 急诊科护患年度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpYearNurseAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 急诊科护患年度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsYearNurseAndPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.Year >= begin.Year && w.Year <= end.Year);
            return await query.ToListAsync();
        }

    }
}
