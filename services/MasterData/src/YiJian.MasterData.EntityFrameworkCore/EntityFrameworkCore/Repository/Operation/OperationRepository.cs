using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore;

public class OperationRepository : MasterDataRepositoryBase<Operation, Guid>, IOperationRepository
{
    public OperationRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    /// <summary>
    /// 分页获取数据
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="sorting"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<List<Operation>> GetPageListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string sorting = null,
        string filter = null)
    {
        skipCount = skipCount == 0 ? 0 : skipCount - 1;
        return await (await GetDbSetAsync())
            .AsNoTracking()
            .WhereIf(
                !filter.IsNullOrWhiteSpace(),
                d =>
                    d.OperationCode.Contains(filter) || d.OperationName.Contains(filter) || filter.Contains(d.OperationCode) || filter.Contains(d.OperationName) || d.PyCode.Contains(filter))
            .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(Operation.Sort) : sorting)
            .PageBy(skipCount, maxResultCount)
            .ToListAsync();
    }


}