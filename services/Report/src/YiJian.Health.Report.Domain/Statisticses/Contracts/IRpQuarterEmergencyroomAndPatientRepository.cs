using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 抢救室滞留时间中位数季度比
    /// </summary>
    public interface IRpQuarterEmergencyroomAndPatientRepository : IRepository<StatisticsQuarterEmergencyroomAndPatient, int>
    {
        /// <summary>
        /// 抢救室滞留时间中位数季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsQuarterEmergencyroomAndPatient>> GetListAsync(DateTime begin, DateTime end);
    }
}
