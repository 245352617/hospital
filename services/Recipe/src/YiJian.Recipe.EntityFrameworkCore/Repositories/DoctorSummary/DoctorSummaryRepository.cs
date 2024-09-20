using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipes.InviteDoctor;

namespace YiJian.Recipe.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 会诊纪要医生 仓储实现
    /// </summary> 
    public class DoctorSummaryRepository : RecipeRepositoryBase<DoctorSummary, Guid>, IDoctorSummaryRepository
    {
        #region constructor
        public DoctorSummaryRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
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
                    i =>
                        i.Name.Contains(filter))
                .LongCountAsync();
        }
        #endregion GetCount

        #region GetList
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<List<DoctorSummary>> GetListAsync(
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    i =>
                        i.Name.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "GroupConsultationId" : sorting)
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
        /// <param name="sorting"></param>
        /// <returns></returns>
        public async Task<List<DoctorSummary>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    i =>
                        i.Name.Contains(filter))
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "GroupConsultationId" : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }
        #endregion GetPagedList
    }
}
