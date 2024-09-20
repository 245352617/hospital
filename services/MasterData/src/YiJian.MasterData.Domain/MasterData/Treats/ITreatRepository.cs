using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Treats;


/// <summary>
/// 诊疗项目字典 仓储接口
/// </summary>  
public interface ITreatRepository : IRepository<Treat, int>
{
    /// <summary>
    /// 根据筛选获取总记录数
    /// </summary>        
    Task<long> GetCountAsync(List<string> categoryCode,string filter = null, PlatformType platformType = PlatformType.EmergencyTreatment);

    /// <summary>
    /// 获取列表记录
    /// </summary>    
    Task<List<Treat>> GetListAsync(
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All, string categoryCode = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>   
    Task<List<Treat>> GetPagedListAsync(
        List<string> categoryCode,
        int skipCount = 0,
        int maxResultCount = int.MaxValue,
        string filter = null,
        string sorting = null,PlatformType platformType = PlatformType.EmergencyTreatment);        
}