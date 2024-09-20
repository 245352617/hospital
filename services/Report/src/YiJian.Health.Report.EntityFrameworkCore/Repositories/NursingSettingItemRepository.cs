using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.NursingSettings.Contracts;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 护理单配置项
    /// </summary>
    public class NursingSettingItemRepository : EfCoreRepository<ReportDbContext, NursingSettingItem, Guid>, INursingSettingItemRepository
    {
        /// <summary>
        /// 护理单配置项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public NursingSettingItemRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

    }
}
