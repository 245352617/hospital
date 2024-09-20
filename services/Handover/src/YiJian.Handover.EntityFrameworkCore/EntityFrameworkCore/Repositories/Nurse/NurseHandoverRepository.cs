namespace YiJian.Handover.EntityFrameworkCore.Repositories
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Linq.Dynamic.Core;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;
    using YiJian.Handover;
    using YiJian.Handover;

    /// <summary>
    /// 护士交班 仓储实现
    /// </summary> 
    public class NurseHandoverRepository : HandoverRepositoryBase<NurseHandover, Guid>, INurseHandoverRepository
    {
        #region constructor

        public NurseHandoverRepository(IDbContextProvider<HandoverDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        #endregion

        #region GetCount

        /// <summary>
        /// 根据筛选获取总记录数
        /// </summary>  
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<long> GetCountAsync(string filter = null, string startDate = null, string endDate = null)
        {
            return await (await GetDbSetAsync())
                .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                .LongCountAsync();
        }

        #endregion GetCount

        #region GetList

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="sorting"></param>
        /// <param name="filter"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public async Task<List<NurseHandover>> GetListAsync(
            string filter = null,
            string sorting = null, string startDate = null, string endDate = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    n =>
                        n.PatientName.Contains(filter)) .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                .OrderBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
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
        public async Task<List<NurseHandover>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null, string startDate = null, string endDate = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(!startDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(startDate) <= w.HandoverDate)
                .WhereIf(!endDate.IsNullOrWhiteSpace(),
                    w => DateTime.Parse(endDate) >= w.HandoverDate)
                .PageBy(skipCount, maxResultCount)
                .OrderByDescending(o => o.CreationTime)
                .ToListAsync();
        }

        #endregion GetPagedList
    }
}