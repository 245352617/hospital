using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Exams;
namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检查明细项 仓储实现
/// </summary> 
public class ExamTargetRepository: MasterDataRepositoryBase<ExamTarget, int>, IExamTargetRepository
{
    #region constructor
    public ExamTargetRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                e =>
                    e.TargetCode.Contains(filter))
            .LongCountAsync();
    }
    #endregion GetCount

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="sorting"></param>
    /// <param name="proCode"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<List<ExamTarget>> GetListAsync(string proCode,
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !proCode.IsNullOrWhiteSpace(),
                e =>
                    e.ProjectCode == proCode)
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.TargetCode.Contains(filter)||e.TargetName.Contains(filter))
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
    public async Task<List<ExamTarget>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.TargetCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "TargetCode" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }   
    #endregion GetPagedList
}
