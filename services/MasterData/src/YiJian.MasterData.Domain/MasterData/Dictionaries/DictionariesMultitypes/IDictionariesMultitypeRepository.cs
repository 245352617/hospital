using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型 仓储接口
/// </summary>  
public interface IDictionariesMultitypeRepository : IRepository<DictionariesMultitype, Guid>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null,
        int status = -1,
        DateTime? startTime = null,
        DateTime? endTime = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<DictionariesMultitype>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        int status = -1,
        DateTime? startTime = null,
        DateTime? endTime = null,
        string sorting = null);
}