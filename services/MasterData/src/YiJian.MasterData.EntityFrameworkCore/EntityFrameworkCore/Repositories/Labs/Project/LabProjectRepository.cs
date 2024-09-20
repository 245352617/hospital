using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检验项目 仓储实现
/// </summary> 
public class LabProjectRepository : MasterDataRepositoryBase<LabProject, int>, ILabProjectRepository
{
    #region constructor

    public LabProjectRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    #endregion

    #region GetCount

    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>  
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(string filter = null, PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.ProjectCode.Contains(filter) || l.ProjectName.Contains(filter) || l.PyCode.Contains(filter))
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .LongCountAsync();
    }

    #endregion GetCount

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="cateCode"></param>
    /// <param name="filter"></param>
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<List<LabProject>> GetListAsync(string cateCode,
        string filter = null, PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .WhereIf(
                !cateCode.IsNullOrWhiteSpace(),
                l =>
                    l.CatalogCode == cateCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.ProjectCode.Contains(filter) || l.ProjectName.Contains(filter) || l.PyCode.Contains(filter))
            .Where(x => x.IsActive)
            .OrderBy(x => x.Sort)
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
    public async Task<List<LabProject>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.ProjectCode.Contains(filter) || l.ProjectName.Contains(filter) || l.PyCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "Sort" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}