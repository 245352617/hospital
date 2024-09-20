using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查明细项 仓储接口
/// </summary>  
public interface IExamTargetRepository : IRepository<ExamTarget, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<ExamTarget>> GetListAsync(string proCode,
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<ExamTarget>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null);        
}