using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科护患季度比
    /// </summary>
    public interface IRpQuarterNurseAndPatientRepository : IRepository<StatisticsQuarterNurseAndPatient, int>
    {

        /// <summary>
        /// 急诊科护患季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsQuarterNurseAndPatient>> GetListAsync(DateTime begin, DateTime end);
    }
}
