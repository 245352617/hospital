using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.Health.Report.EntityFrameworkCore;
using YiJian.Health.Report.Enums;
using YiJian.Health.Report.NursingDocuments.Entities;
using YiJian.Health.Report.NursingSettings.Contracts;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 出入量配置
    /// </summary>
    public class IntakeSettingRepository : EfCoreRepository<ReportDbContext, IntakeSetting, Guid>, IIntakeSettingRepository
    {
        /// <summary>
        /// 出入量配置
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public IntakeSettingRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }
        /// <summary>
        ///获取出入量配置集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<IntakeSetting>> GetIntakeSettingListAsync(int? intakeType, bool? isEnabled, string keywords)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.WhereIf(intakeType.HasValue, p => p.IntakeType == (EIntakeType)intakeType)
                .WhereIf(isEnabled.HasValue, p => p.IsEnabled == isEnabled)
                .WhereIf(!string.IsNullOrWhiteSpace(keywords), p => p.Content.Contains(keywords) || p.Code.Contains(keywords))
                .OrderBy(o => o.Sort)
                .ThenByDescending(o => o.CreationTime)
                .ToListAsync();
            return query;
        }
    }
}
