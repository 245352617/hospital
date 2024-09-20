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
    /// 急诊抢救室患者死亡率月度比
    /// </summary>
    public class RpMonthEmergencyroomAndDeathPatientRepository : EfCoreRepository<ReportDbContext, StatisticsMonthEmergencyroomAndDeathPatient, int>, IRpMonthEmergencyroomAndDeathPatientRepository
    {
        /// <summary>
        /// 急诊科医患月度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpMonthEmergencyroomAndDeathPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 急诊抢救室患者死亡率月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsMonthEmergencyroomAndDeathPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.YearMonth >= begin && w.YearMonth <= end);
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取急诊抢救室患者死亡率信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<ViewAdmissionTransfeRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end)
        {
            var ctx = await GetDbContextAsync();
            var query = ctx.ViewAdmissionTransfeRecords.Where(w => w.VisitDate >= begin && w.VisitDate <= end && w.FromAreaCode == "RescueArea")
                .OrderBy(o => o.TriageLevelName);
            var data = await query.ToListAsync();

            int index = 1;
            foreach (var item in data)
            {
                item.Row = index++;
            }
            return data;
        }

    }
}
