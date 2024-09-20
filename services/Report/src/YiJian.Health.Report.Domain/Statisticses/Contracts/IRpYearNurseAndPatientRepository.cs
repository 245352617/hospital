using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科护患年度比
    /// </summary>
    public interface IRpYearNurseAndPatientRepository : IRepository<StatisticsYearNurseAndPatient, int>
    {
        /// <summary>
        /// 急诊科护患年度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsYearNurseAndPatient>> GetListAsync(DateTime begin, DateTime end);
    }
}
