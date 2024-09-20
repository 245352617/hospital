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
    /// 急诊科护患月度比
    /// </summary>
    public class RpMonthNurseAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsMonthNurseAndPatient, int>, IRpMonthNurseAndPatientRepository
    {
        /// <summary>
        /// 急诊科护患月度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpMonthNurseAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }


        /// <summary>
        /// 急诊科护患月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsMonthNurseAndPatient>> GetListAsync(DateTime begin, DateTime end)
        {
            var db = await GetDbSetAsync();
            var query = db.Where(w => w.YearMonth >= begin && w.YearMonth <= end);
            return await query.ToListAsync();
        }


        /// <summary>
        /// 获取所在时间区间内所有医生的接诊汇总数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<UspNursePatientRatio>> GetUspNursePatientRatiosAsync(DateTime begin, DateTime end)
        {
            var ctx = await GetDbContextAsync();
            var sql = $"EXEC usp_nursePatientRatio N'{begin.ToString("yyyy-MM-dd HH:mm:ss")}',N'{end.ToString("yyyy-MM-dd HH:mm:ss")}'";
            var query = ctx.UspNursePatientRatios.FromSqlRaw<UspNursePatientRatio>(sql);
            var data = await query.ToListAsync();

            int index = 1;
            foreach (var item in data)
            {
                item.Row = index++;
            }

            return data;
        }

        /// <summary>
        /// 接诊病人详细视图
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<ViewAdmissionRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end)
        {
            var ctx = await GetDbContextAsync();
            var query = ctx.ViewAdmissionRecords.Where(w => w.VisitDate >= begin && w.VisitDate <= end && w.DutyNurseCode != "" && w.DutyNurseCode != null)
                .OrderBy(o=>o.DutyNurseCode);
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
