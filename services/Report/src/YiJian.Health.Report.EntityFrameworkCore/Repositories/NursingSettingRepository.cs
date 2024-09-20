using Microsoft.EntityFrameworkCore; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
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
    /// 护理单配置
    /// </summary>
    public class NursingSettingRepository : EfCoreRepository<ReportDbContext, NursingSetting, Guid>, INursingSettingRepository
    {
        /// <summary>
        /// 护理单配置
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public NursingSettingRepository(IDbContextProvider<ReportDbContext> dbContextProvider)
        : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取所有的护理单主题集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<NursingSetting>> GetAllNursingSheetListAsync()
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .Include(i => i.Headers)
                .OrderByDescending(o => o.Sort)
                .ThenByDescending(o => o.CreationTime)
                .ToListAsync();
            return query;
        }


        /// <summary>
        /// 根据分组ID获取护理单主题集合
        /// </summary>
        /// <returns></returns>
        public async Task<List<NursingSetting>> GetNursingSheetListAsync(string groupId)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet.WhereIf(!string.IsNullOrWhiteSpace(groupId),p=>p.GroupId== groupId)
                .Include(i => i.Headers)
                .OrderByDescending(o => o.Sort)
                .ThenByDescending(o=>o.CreationTime)
                .ToListAsync();
            return query;
        }

        /// <summary>
        /// 获取完整的配置项集合(最多三层,不存一个表了，分开处理，层级结构太复杂不想弄自关联)
        /// </summary> 
        /// <param name="isDynamicSix">是否是动态六项</param>
        /// <param name="headerIds">指定到表头(有只查指定的header下面的表单域，无则查所有的)</param>
        /// <param name="notinheaderIds"></param> 
        /// <returns></returns>
        public async Task<List<NursingSetting>> GetAllSettingsAsync(bool isDynamicSix, List<Guid> headerIds,List<Guid> notinheaderIds)
        {
            var dbSet = await GetDbSetAsync();
            var query = await dbSet
                .IncludeIf(isDynamicSix && headerIds.Count>0, i => i.Headers.Where(w => headerIds.Contains(w.Id))) //特殊记录查询
                .IncludeIf(!isDynamicSix && notinheaderIds.Count > 0, i=>i.Headers.Where(w => !notinheaderIds.Contains(w.Id)))
                .Include(i=>i.Headers)
                .ThenInclude(i => i.Items
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
                .OrderByDescending(o => o.Sort)
                .ThenByDescending(o => o.CreationTime) 
                .ToListAsync();
            return query;
        }

    }
}
