using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊抢救室患者死亡率季度比
    /// </summary>
    public interface IRpQuarterEmergencyroomAndDeathPatientRepository : IRepository<StatisticsQuarterEmergencyroomAndDeathPatient, int>
    {
        /// <summary>
        /// 急诊抢救室患者死亡率季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsQuarterEmergencyroomAndDeathPatient>> GetListAsync(DateTime begin, DateTime end);
    }
}
