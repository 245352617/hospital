using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 急诊抢救室患者死亡率月度比
    /// </summary>
    public interface IRpMonthEmergencyroomAndDeathPatientRepository : IRepository<StatisticsMonthEmergencyroomAndDeathPatient, int>
    {
        /// <summary>
        /// 急诊抢救室患者死亡率月度比
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<StatisticsMonthEmergencyroomAndDeathPatient>> GetListAsync(DateTime begin, DateTime end);

        /// <summary>
        /// 获取急诊抢救室患者死亡率信息
        /// </summary>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public Task<List<ViewAdmissionTransfeRecord>> GetViewAdmissionRecordsAsync(DateTime begin, DateTime end);
    }
}
