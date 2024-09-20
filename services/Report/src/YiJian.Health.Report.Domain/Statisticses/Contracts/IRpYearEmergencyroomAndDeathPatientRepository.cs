using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊抢救室患者死亡率年度比
    /// </summary>
    public interface IRpYearEmergencyroomAndDeathPatientRepository : IRepository<StatisticsYearEmergencyroomAndDeathPatient, int>
    {
        /// <summary>
        /// 急诊抢救室患者死亡率年度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsYearEmergencyroomAndDeathPatient>> GetListAsync(DateTime begin, DateTime end);

    }
}
