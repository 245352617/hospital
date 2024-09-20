using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.Statisticses.Entities;
using YiJian.Health.Report.Statisticses.Models;

namespace YiJian.Health.Report.Statisticses.Contracts
{
    /// <summary>
    /// 统计分析每日采集
    /// </summary>
    public interface IStatisticsRepository : IRepository<StatisticsArea, int>
    {
        /// <summary>
        /// 获取患者记录数(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<int> GetAdmissionRecordCountAsync(InputAdmissionRecordModel model);

        /// <summary>
        /// 分页获取患者列表(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<AdmissionRecord>> GetAdmissionRecordPageAsync(InputAdmissionRecordByPageModel model);

        /// <summary>
        /// 获取患者列表(患者，诊断，病历)
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<List<AdmissionRecord>> GetAdmissionRecordAsync(InputAdmissionRecordModel model);
    }
}
