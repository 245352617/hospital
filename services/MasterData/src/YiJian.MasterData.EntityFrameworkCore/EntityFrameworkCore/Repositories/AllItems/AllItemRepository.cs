using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.AllItems;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 诊疗检查检验药品项目合集 仓储实现
/// </summary> 
public class AllItemRepository : MasterDataRepositoryBase<AllItem, int>, IAllItemRepository
{
    #region constructor

    public AllItemRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                a =>
                    a.Code.Contains(filter) || a.Name.Contains(filter))
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
    public async Task<List<AllItem>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                a =>
                    a.CategoryCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "CategoryCode" : sorting)
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
    /// <param name="categoryCode"></param>
    /// <param name="typeCope">站点</param>
    /// <returns></returns>
    public async Task<List<AllItem>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null,
        string categoryCode = null,
        string typeCope = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(!categoryCode.IsNullOrWhiteSpace(), x => x.CategoryCode == categoryCode)
            .WhereIf(!typeCope.IsNullOrWhiteSpace(), x => x.TypeCode == typeCope)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                a =>
                    a.Code.Contains(filter) || a.Name.Contains(filter) || a.PY.Contains(filter.ToUpper()))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}