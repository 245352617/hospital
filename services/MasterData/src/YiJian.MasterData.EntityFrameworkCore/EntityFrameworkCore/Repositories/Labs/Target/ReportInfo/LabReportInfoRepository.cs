using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Labs;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Labs.Target.ReportInfo
{
    internal class LabReportInfoRepository : MasterDataRepositoryBase<LabReportInfo, int>, ILabReportInfoRepository
    {
        public LabReportInfoRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

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
                    l =>
                        l.Name.Contains(filter))
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
        public async Task<List<LabReportInfo>> GetListAsync(string filter = null, string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    l =>
                        l.Name.Contains(filter) || l.CatelogName.Contains(filter))
                .OrderBy(f => f.Code)
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
        public async Task<List<LabReportInfo>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null,
            string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    l =>
                        l.Name.Contains(filter))
                .OrderBy(f => f.Code)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }

        #endregion GetPagedList
    }
}
