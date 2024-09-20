using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊科医患季度比
    /// </summary>
    public interface IRpQuarterDoctorAndPatientRepository : IRepository<StatisticsQuarterDoctorAndPatient, int>
    {

        /// <summary>
        /// 查看急诊科医患季度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsQuarterDoctorAndPatient>> GetListAsync(DateTime begin, DateTime end);
    }
}
