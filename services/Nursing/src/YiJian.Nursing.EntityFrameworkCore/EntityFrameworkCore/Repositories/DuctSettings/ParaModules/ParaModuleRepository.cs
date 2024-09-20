using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Settings;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 表:模块参数 仓储实现
    /// </summary> 
    public class ParaModuleRepository : NursingRepositoryBase<ParaModule, Guid>, IParaModuleRepository
    {
        #region constructor

        public ParaModuleRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        #endregion

        #region GetCount

        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>  
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null)
        {
            return await (await GetDbSetAsync())
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    p =>
                        p.ModuleCode.Contains(filter))
                .LongCountAsync();
        }

        #endregion GetCount

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="moduleType"></param>
        /// <param name="query"></param>
        /// <param name="moduleCode"></param>
        /// <returns></returns>
        public async Task<List<ParaModule>> GetListAsync(string moduleType, string query = null, string moduleCode = "")
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(moduleType), s => s.ModuleType == moduleType)
                .WhereIf(!string.IsNullOrWhiteSpace(query),
                    s => s.ModuleName.Contains(query) || s.Py.Contains(query))
                .WhereIf(!string.IsNullOrWhiteSpace(moduleCode),
                    s => s.ModuleCode == moduleCode)
                .OrderBy(x => x.Sort)
                .ToListAsync();
        }

        #endregion GetList

        #region GetPagedList

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<ParaModule>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    p =>
                        p.ModuleCode.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "ModuleCode" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        #endregion GetPagedList
    }
}