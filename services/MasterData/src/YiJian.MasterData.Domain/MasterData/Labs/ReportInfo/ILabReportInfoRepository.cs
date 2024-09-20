using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Labs;

/// <summary>
/// 检验单信息 仓储接口
/// </summary>  
public interface ILabReportInfoRepository : IRepository<LabReportInfo, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<LabReportInfo>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<LabReportInfo>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null);        
}