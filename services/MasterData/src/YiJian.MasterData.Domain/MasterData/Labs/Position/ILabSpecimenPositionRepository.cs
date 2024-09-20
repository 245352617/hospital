using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.Labs.Position;


/// <summary>
/// 检验标本采集部位 仓储接口
/// </summary>  
public interface ILabSpecimenPositionRepository : IRepository<LabSpecimenPosition, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string positionCode,string filter = null);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<LabSpecimenPosition>> GetListAsync(string positionCode,
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<LabSpecimenPosition>> GetPagedListAsync(string positionCode,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null);        
}