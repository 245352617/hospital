using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科护患月度比
    /// </summary>
    public interface IRpMonthNurseAndPatientRepository : IRepository<StatisticsMonthNurseAndPatient, int>
    {
        /// <summary>
        /// 急诊科护患月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsMonthNurseAndPatient>> GetListAsync(DateTime begin, DateTime end);

        /// <summary>
        /// 获取所在时间区间内所有医生的接诊汇总数据
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<UspNursePatientRatio>> GetUspNursePatientRatiosAsync(DateTime begin, DateTime end);

        /// <summary>
        /// 接诊病人详细视图
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<ViewAdmissionRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end);
    }
}
