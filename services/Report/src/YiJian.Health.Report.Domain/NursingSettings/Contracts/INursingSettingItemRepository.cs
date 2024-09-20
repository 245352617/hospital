using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.NursingSettings.Contracts
{ 
    /// <summary>
    /// 护理单配置项
    /// </summary>
    public interface INursingSettingItemRepository : IRepository<NursingSettingItem, Guid>
    {

    }

}
