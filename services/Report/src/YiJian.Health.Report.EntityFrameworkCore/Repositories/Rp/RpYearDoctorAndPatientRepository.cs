using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Entities;
using System.Linq.Dynamic.Core;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Rp
{
    /// <summary>
    /// 急诊科医患年度视比
    /// </summary>
    public class RpYearDoctorAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsYearDoctorAndPatient, int>, IRpYearDoctorAndPatientRepository
    {
        /// <summary>
        /// 急诊科医患年度视比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpYearDoctorAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
         
        /// <summary>
        /// 查看急诊科医患年度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsYearDoctorAndPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.Year >= begin.Year && w.Year <= end.Year);
            return await query.ToListAsync();
        }
    }
}
