using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingDocuments.Contracts;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.ReportDatas;
using YiJian.Health.Report.Statisticses.Contracts;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Rp
{
    /// <summary>
    /// 急诊科医患月度比
    /// </summary>
    public class RpMonthDoctorAndPatientRepository : EfCoreRepository<ReportDbContext, StatisticsMonthDoctorAndPatient, int>, IRpMonthDoctorAndPatientRepository
    {
        /// <summary>
        /// 急诊科医患月度比
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public RpMonthDoctorAndPatientRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 查询指定范围内的数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<StatisticsMonthDoctorAndPatient>> GetListAsync(DateTime begin, DateTime end)
        { 
            var db = await GetDbSetAsync();
            var query = db.Where(w => (w.YearMonth >= begin && w.YearMonth <= end)); 
            return await query.ToListAsync();
        }

        /// <summary>
        /// 获取所在时间区间内所有医生的接诊汇总数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public async Task<List<UspDoctorPatientRatio>> GetUspDoctorPatientRatiosAsync(DateTime begin, DateTime end)
        {
            var ctx = await GetDbContextAsync();
            var sql = $"EXEC usp_doctorPatientRatio N'{begin.ToString("yyyy-MM-dd HH:mm:ss")}',N'{end.ToString("yyyy-MM-dd HH:mm:ss")}'";
            var query = ctx.UspDoctorPatientRatios.FromSqlRaw<UspDoctorPatientRatio>(sql);
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
            var query = ctx.ViewAdmissionRecords.Where(w=>w.VisitDate>= begin && w.VisitDate <= end && w.FirstDoctorCode.Trim() != "" && w.FirstDoctorCode != null) 
                .OrderBy(o=>o.FirstDoctorCode);
            var data = await query.ToListAsync();
            
            int index = 1;
            foreach (var item in data)
            {
                item.Row = index++;
            }
            return data;
        }

        /// <summary>
        /// 调用存储过程采集数据入口
        /// </summary>
        /// <param name="reportType"></param>
        /// <param name="veidoo"></param>
        /// <param name="formatDate"></param>
        /// <returns></returns>
        public async Task<int> CallUspStatisticsAsync(int reportType, int veidoo, string formatDate)
        {
            var ctx = await GetDbContextAsync();
            var sql = $"EXEC usp_statistics @reportType = {reportType}, @veidoo = {veidoo}, @formatDate = N'{formatDate}' ";
            var result = await ctx.Database.ExecuteSqlRawAsync(sql);
            return result;
        }


    }
}
