using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.AllItems;

/// <summary>
/// 诊疗检查检验药品项目合集 仓储接口
/// </summary>  
public interface IAllItemRepository : IRepository<AllItem, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<AllItem>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<AllItem>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null,
        string categoryCode = null,
        string typeCope=null);        
}