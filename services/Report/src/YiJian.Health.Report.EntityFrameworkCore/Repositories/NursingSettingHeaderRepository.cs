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
using YiJian.Health.Report.NursingSettings.Contracts;
using YiJian.Health.Report.NursingSettings.Entities;

namespace YiJian.Health.Report.Repositories
{
    /// <summary>
    /// 护理单表头配置
    /// </summary>
    public class NursingSettingHeaderRepository : EfCoreRepository<ReportDbContext, NursingSettingHeader, Guid>, INursingSettingHeaderRepository
    {
        /// <summary>
        /// 护理单表头配置
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public NursingSettingHeaderRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {
              
        }

        /// <summary>
        /// 获取动态六项表单域内容集合
        /// </summary>
        /// <param name="headerId">表头描述 如:气道,循环,出血</param>
        /// <returns></returns>
        public async Task<NursingSettingHeader> GetSixInputOptionsAsync(Guid headerId)
        { 
            var dbSet = await GetDbSetAsync();
            var query = await dbSet 
                .Include(i => i.Items
                    .Where(w => w.Lv == 0)
                    .OrderByDescending(o => o.Sort)
                    .ThenByDescending(o => o.CreationTime))
                .ThenInclude(i => i.Items
                    .Where(w => w.Lv == 1)
                    .OrderByDescending(o => o.Sort)
                    .ThenByDescending(o => o.CreationTime))
                .ThenInclude(i => i.Items
                    .Where(w => w.Lv == 2)
                    .OrderByDescending(o => o.Sort)
                    .ThenByDescending(o => o.CreationTime))
                .Where(w => w.Id == headerId)
                .OrderByDescending(o => o.Sort)
                .ThenByDescending(o => o.CreationTime)
                .FirstOrDefaultAsync();

            return query;
        }

    }
}
