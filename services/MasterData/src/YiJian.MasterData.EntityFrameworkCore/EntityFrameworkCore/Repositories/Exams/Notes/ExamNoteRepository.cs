using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 检查申请注意事项 仓储实现
/// </summary> 
public class ExamNoteRepository: MasterDataRepositoryBase<ExamNote, int>, IExamNoteRepository
{
    #region constructor
    public ExamNoteRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
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
                    e.NoteCode.Contains(filter))
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
    public async Task<List<ExamNote>> GetListAsync(
        string filter = null,
        string sorting = null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                e =>
                    e.NoteCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "NoteCode" : sorting)
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
    public async Task<List<ExamNote>> GetPagedListAsync(
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
                    e.NoteCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "NoteCode" : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }   
    #endregion GetPagedList
}
