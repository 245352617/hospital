using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.Exams;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检查申请项目 仓储实现
/// </summary> 
public class ExamProjectRepository : MasterDataRepositoryBase<ExamProject, int>, IExamProjectRepository
{
    #region constructor

    public ExamProjectRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(
        dbContextProvider)
    {
    }

    #endregion

    #region GetCount

    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>  
    /// <param name="filter"></param>
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<long> GetCountAsync(string filter = null, PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.ProjectCode.Contains(filter) || e.ProjectName.Contains(filter) || e.PyCode.Contains(filter))
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
    public async Task<List<ExamProject>> GetListAsync(
        string cateCode,
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
                e =>
                    e.CatalogCode == cateCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.ProjectName.Contains(filter) || e.ProjectCode.Contains(filter)|| e.PyCode.Contains(filter))
            .Where(x => x.IsActive)
            .OrderBy(o => o.Sort)
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
    /// <param name="platformType"></param>
    /// <returns></returns>
    public async Task<List<ExamProject>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null, PlatformType platformType = PlatformType.All)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.ProjectCode.Contains(filter) || e.ProjectName.Contains(filter) || e.PyCode.Contains(filter))
            .WhereIf(
                platformType != PlatformType.All,
                e =>
                    e.PlatformType == platformType)
            .OrderBy(o => o.Sort)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }

    #endregion GetPagedList
}