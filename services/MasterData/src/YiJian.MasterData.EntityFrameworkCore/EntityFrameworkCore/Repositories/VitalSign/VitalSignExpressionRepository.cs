using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.VitalSign;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 评分项 仓储实现
/// </summary> 
public class VitalSignExpressionRepository: MasterDataRepositoryBase<VitalSignExpression, Guid>, IVitalSignExpressionRepository
{
    #region constructor
    public VitalSignExpressionRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                v =>
                    v.ItemName.Contains(filter))
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
    public async Task<List<VitalSignExpression>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                v =>
                    v.ItemName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "ItemName" : sorting)
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
    public async Task<List<VitalSignExpression>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                v =>
                    v.ItemName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "ItemName" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }   
    #endregion GetPagedList
}
