using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

/// <summary>
/// 检查部位 仓储接口
/// </summary>  
public interface IExamPartRepository : IRepository<ExamPart, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<ExamPart>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<ExamPart>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null);        
}