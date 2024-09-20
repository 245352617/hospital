using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.DictionariesMultitypes;


namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 字典多类型 仓储实现
/// </summary> 
public class DictionariesMultitypeRepository : MasterDataRepositoryBase<DictionariesMultitype, Guid>,
    IDictionariesMultitypeRepository
{
    #region constructor

    public DictionariesMultitypeRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    #endregion

    #region GetCount

    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>  
    /// <param name="filter"></param>
    /// <param name="status"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(string filter = null,
        int status = -1,
        DateTime? startTime = null,
        DateTime? endTime = null)
    {
        return await (await GetDbSetAsync())
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                d =>
                    d.Name.Contains(filter) ||
                    d.Code.Contains(filter))
            .WhereIf(status != -1, x => x.Status == (status == 1))
            .WhereIf(startTime != null,
                x => x.CreationTime >= startTime.Value)
            .WhereIf(endTime != null,
                x => x.CreationTime <= endTime.Value)
            .LongCountAsync();
    }

    #endregion GetCount

    #region GetPagedList

    /// <summary>
    /// 获取分页数据
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="filter"></param>
    /// <param name="status"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="sorting"></param>
    /// <returns></returns>
    public async Task<List<DictionariesMultitype>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        int status = -1,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                d =>
                    d.Name.Contains(filter) ||
                    d.Code.Contains(filter))
            .WhereIf(status != -1, x => x.Status == (status == 1))
            .WhereIf(startTime != null,
                x => x.CreationTime >= startTime.Value)
            .WhereIf(endTime != null,
                x => x.CreationTime <= endTime.Value)
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}