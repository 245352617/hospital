using System;
using System.Linq;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

/// <summary>
/// 药品用法字典 仓储实现
/// </summary> 
public class MedicineUsageRepository : MasterDataRepositoryBase<MedicineUsage, int>, IMedicineUsageRepository
{
    #region constructor

    public MedicineUsageRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(
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
                    m.UsageCode.Contains(filter) || m.UsageName.Contains(filter) || m.PyCode.Contains(filter))
            .LongCountAsync();
    }

    #endregion GetCount

    #region GetList

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="sorting"></param>
    /// <param name="filter"></param>
    /// <param name="addCard"></param>
    /// <returns></returns>
    public async Task<List<MedicineUsage>> GetListAsync(
        string filter = null,
        string sorting = null,string addCard=null)
    {
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                m =>
                    m.UsageName.Contains(filter) || m.PyCode.Contains(filter))
            .WhereIf(!string.IsNullOrEmpty(addCard),x=>x.AddCard==addCard)
            .OrderBy(sorting.IsNullOrWhiteSpace() ? "UsageCode" : sorting) 
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
    public async Task<List<MedicineUsage>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null)
    {
        //return await (await GetDbSetAsync())
        //    .AsNoTracking()
        //    .WhereIf(
        //        !filter.IsNullOrWhiteSpace(),
        //        m =>
        //            m.UsageCode.Contains(filter) || m.UsageName.Contains(filter) || m.PyCode.Contains(filter))
        //    .OrderBy(sorting.IsNullOrWhiteSpace() ? "Sort" : sorting)
        //    .PageBy(skipCount, maxResultCount)
        //    .ToListAsync();

        var db = await GetDbSetAsync(); 
        var listA = db.AsNoTracking()
            .WhereIf(!filter.IsNullOrWhiteSpace(), w => w.UsageCode.Contains(filter)); 

        var listB = db.AsNoTracking()
            .WhereIf(!filter.IsNullOrWhiteSpace(), w => w.UsageName.Contains(filter)); 

        var listC = db.AsNoTracking()
            .WhereIf(!filter.IsNullOrWhiteSpace(), w => w.PyCode.Contains(filter)); 
        
        var query =  listA.Union(listB).Union(listC); 

        return await query.PageBy(skipCount,maxResultCount).ToListAsync();
    }

    #endregion GetPagedList
}