using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.EMR.ApplicationSettings.Contracts;
using YiJian.EMR.ApplicationSettings.Entities;
using YiJian.EMR.EntityFrameworkCore;

namespace YiJian.EMR.Repositories
{
    /// <summary>
    /// 引用配置仓储
    /// </summary>
    public class AppSettingRepository : EfCoreRepository<EMRDbContext, AppSetting, Guid>, IAppSettingRepository
    {
        public AppSettingRepository(IDbContextProvider<EMRDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
    }
}
