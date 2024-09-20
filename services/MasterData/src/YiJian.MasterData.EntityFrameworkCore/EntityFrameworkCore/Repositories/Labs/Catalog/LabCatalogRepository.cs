using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Labs;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检验目录 仓储实现
/// </summary> 
public class LabCatalogRepository : MasterDataRepositoryBase<LabCatalog, int>, ILabCatalogRepository
{
    #region constructor

    public LabCatalogRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                l =>
                    l.CatalogCode.Contains(filter))
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
    public async Task<List<LabCatalog>> GetListAsync(string filter = null, string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.CatalogCode.Contains(filter) || l.CatalogName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "Sort" : sorting)
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
    public async Task<List<LabCatalog>> GetPagedListAsync(
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
                    l.CatalogCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "CatalogCode" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}