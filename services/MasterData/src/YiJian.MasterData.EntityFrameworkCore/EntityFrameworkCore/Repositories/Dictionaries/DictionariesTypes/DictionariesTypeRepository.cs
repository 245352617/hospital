using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.DictionariesTypes;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 字典类型编码 仓储实现
/// </summary> 
public class DictionariesTypeRepository : MasterDataRepositoryBase<DictionariesTypes.DictionariesType, Guid>, IDictionariesTypeRepository
{
    #region constructor

    public DictionariesTypeRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(
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
                    d.DictionariesTypeName.Contains(filter) ||
                    d.DictionariesTypeCode.Contains(filter))
            .WhereIf(status != -1, x => x.Status == (status == 1))
            .WhereIf(startTime != null,
                x => x.CreationTime >= startTime.Value)
            .WhereIf(endTime != null,
                x => x.CreationTime <= endTime.Value)
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
    public async Task<List<DictionariesTypes.DictionariesType>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                d =>
                    d.DictionariesTypeCode == filter)
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
    /// <param name="status"></param>
    /// <param name="startTime"></param>
    /// <param name="endTime"></param>
    /// <param name="sorting"></param>
    /// <returns></returns>
    public async Task<List<DictionariesTypes.DictionariesType>> GetPagedListAsync(
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
                    d.DictionariesTypeName.Contains(filter) ||
                    d.DictionariesTypeCode.Contains(filter))
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