using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科各级患者比例月度比
    /// </summary>
    public interface IRpMonthLevelAndPatientRepository : IRepository<StatisticsMonthLevelAndPatient, int>
    {
        /// <summary>
        /// 急诊科各级患者比例月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsMonthLevelAndPatient>> GetListAsync(DateTime begin, DateTime end);

        /// <summary>
        /// 获取急诊科各级患者比例的记录信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<ViewAdmissionTransfeRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end);

    }
}
