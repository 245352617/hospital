using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Labs.Position;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检验标本采集部位 仓储实现
/// </summary> 
public class LabSpecimenPositionRepository : MasterDataRepositoryBase<LabSpecimenPosition, int>, ILabSpecimenPositionRepository
{
    #region constructor
    public LabSpecimenPositionRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
    #endregion

    #region GetCount
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>  
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(string positionCode, string filter = null)
    {
        return await (await GetDbSetAsync())
               .WhereIf(
                !positionCode.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenCode == positionCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenPartCode.Contains(filter) || l.SpecimenPartName.Contains(filter) || l.PyCode.Contains(filter))
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
    public async Task<List<LabSpecimenPosition>> GetListAsync(string positionCode,
    string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !positionCode.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenCode == positionCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenPartCode.Contains(filter) || l.SpecimenPartName.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "SpecimenCode" : sorting)
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
    public async Task<List<LabSpecimenPosition>> GetPagedListAsync(string positionCode,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !positionCode.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenCode == positionCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                l =>
                    l.SpecimenPartCode.Contains(filter) || l.SpecimenPartName.Contains(filter) || l.PyCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "CreationTime" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }
    #endregion GetPagedList
}
