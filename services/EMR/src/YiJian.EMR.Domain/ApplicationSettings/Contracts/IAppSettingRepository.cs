using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;
using YiJian.EMR.ApplicationSettings.Entities;

namespace YiJian.EMR.ApplicationSettings.Contracts
{
    /// <summary>
    /// 都昌IP配置
    /// </summary>
    public interface IAppSettingRepository : IRepository<AppSetting, Guid>
    {

    }
}
