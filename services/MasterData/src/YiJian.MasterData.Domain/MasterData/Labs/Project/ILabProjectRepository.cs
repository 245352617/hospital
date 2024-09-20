using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData;


/// <summary>
/// 检验项目 仓储接口
/// </summary>  
public interface ILabProjectRepository : IRepository<LabProject, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(string filter = null, PlatformType platformType = PlatformType.All);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<LabProject>> GetListAsync(string cateCode,
        string filter = null, PlatformType platformType = PlatformType.All);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<LabProject>> GetPagedListAsync(
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All);        
}