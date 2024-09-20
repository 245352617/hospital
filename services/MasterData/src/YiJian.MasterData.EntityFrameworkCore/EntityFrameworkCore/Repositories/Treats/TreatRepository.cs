using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Treats;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 诊疗项目字典 仓储实现
/// </summary> 
public class TreatRepository : MasterDataRepositoryBase<Treat, int>, ITreatRepository
{
    #region constructor

    public TreatRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    #endregion

    #region GetCount

    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>  
    /// <param name="filter"></param>
    /// <param name="categoryCode"></param>
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(List<string> categoryCode, string filter = null,
        PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.TreatName.Contains(filter) || t.PyCode.Contains(filter))
            .WhereIf(categoryCode.Any(), w => categoryCode.Contains(w.CategoryCode))
            .WhereIf(platformType != PlatformType.All, t => t.PlatformType == platformType)
            .LongCountAsync();
    }

    #endregion GetCount

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="sorting"></param>
    /// <param name="filter"></param>
    /// <param name="platformType"></param>
    /// <param name="categoryCode"></param>
    /// <returns></returns>
    public async Task<List<Treat>> GetListAsync(
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All, string categoryCode = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                t =>
                    t.TreatName.Contains(filter) || t.PyCode.Contains(filter))
            .WhereIf(
                !categoryCode.IsNullOrWhiteSpace(),
                t =>
                    categoryCode.Contains(t.CategoryCode))
            .WhereIf(platformType != PlatformType.All, x => x.PlatformType == platformType)
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
    /// <param name="sorting"></param>
    /// <param name="categoryCode"></param>
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<List<Treat>> GetPagedListAsync(
        List<string> categoryCodes,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null,
        PlatformType platformType = PlatformType.All)
    {
        string filterEmpty = filter.IsNullOrEmpty() ? "" : filter.ToLower();
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(!filter.IsNullOrWhiteSpace(), t => t.TreatName.Contains(filter) || t.PyCode.Contains(filter))
            .WhereIf(categoryCodes.Any(), t => categoryCodes.Contains(t.CategoryCode))
            .WhereIf(platformType != PlatformType.All, t => t.PlatformType == platformType)
            .OrderByDescending(t => t.PyCode.ToLower().StartsWith(filterEmpty) || t.TreatName.StartsWith(filterEmpty) ? 1 : 0)
            .ThenBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}