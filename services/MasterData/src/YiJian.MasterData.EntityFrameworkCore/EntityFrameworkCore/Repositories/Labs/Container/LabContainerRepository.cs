using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Labs.Container;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 容器编码 仓储实现
/// </summary> 
public class LabContainerRepository: MasterDataRepositoryBase<LabContainer, int>, ILabContainerRepository
{
    #region constructor
    public LabContainerRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                    l.ContainerCode.Contains(filter))
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
    public async Task<List<LabContainer>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.ContainerCode.Contains(filter)||l.ContainerName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "ContainerCode" : sorting)
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
    public async Task<List<LabContainer>> GetPagedListAsync(
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
                    l.ContainerCode.Contains(filter)||l.ContainerName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "ContainerCode" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }   
    #endregion GetPagedList
}
