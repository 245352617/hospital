using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.NursingSettings.Contracts
{
    /// <summary>
    /// 出入量配置
    /// </summary>
    public interface IIntakeSettingRepository : IRepository<IntakeSetting, Guid>
    {
        /// <summary>
        ///获取出入量配置集合
        /// </summary>
        /// <returns></returns>
        Task<List<IntakeSetting>> GetIntakeSettingListAsync(int? intakeType, bool? isEnabled, string keywords);
    }
}
