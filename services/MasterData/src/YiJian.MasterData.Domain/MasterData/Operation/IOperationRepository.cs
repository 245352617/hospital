using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

public interface IOperationRepository: IRepository<Operation, Guid>
{
    /// <summary>
    /// 分页获取数据
    /// </summary>
    /// <param name="skipCount"></param>
    /// <param name="maxResultCount"></param>
    /// <param name="sorting"></param>
    /// <param name="filter"></param>
    /// <returns></returns>
     Task<List<Operation>> GetPageListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string sorting = null,
        string filter = null);
}