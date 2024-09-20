using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 药品频次字典 仓储实现
/// </summary> 
public class MedicineFrequencyRepository : MasterDataRepositoryBase<MedicineFrequency, int>,
    IMedicineFrequencyRepository
{
    #region constructor

    public MedicineFrequencyRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(
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
    public async Task<long> GetCountAsync(string filter = null)
    {
        return await (await GetDbSetAsync())
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                m =>
                    m.FrequencyCode.Contains(filter) || m.FrequencyName.Contains(filter))
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
    public async Task<List<MedicineFrequency>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        string filterEmpty = filter.IsNullOrEmpty() ? "" : filter.ToLower();
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(!filterEmpty.IsNullOrWhiteSpace(), m => m.FrequencyCode.Contains(filterEmpty) || m.FrequencyName.Contains(filterEmpty))
            .Where(x => x.IsActive && !string.IsNullOrEmpty(x.FrequencyCode) && !string.IsNullOrEmpty(x.FrequencyName))
            // 使得完全匹配数据排在前面
            .OrderByDescending(f => f.FrequencyCode.ToLower() == filterEmpty || f.FrequencyName.ToLower() == filterEmpty ? 1 : 0)
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
    public async Task<List<MedicineFrequency>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null)
    {
        string filterEmpty = filter.IsNullOrEmpty() ? "" : filter.ToLower();
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(!filterEmpty.IsNullOrWhiteSpace(), m => m.FrequencyCode.Contains(filterEmpty) || m.FrequencyName.Contains(filterEmpty))
            .Where(x => !string.IsNullOrEmpty(x.FrequencyCode) && !string.IsNullOrEmpty(x.FrequencyName))
            // 使得完全匹配数据排在前面
            .OrderByDescending(f => f.FrequencyCode.ToLower() == filterEmpty || f.FrequencyName.ToLower() == filterEmpty ? 1 : 0)
            .ThenBy(sorting.IsNullOrWhiteSpace() ? "Sort" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}