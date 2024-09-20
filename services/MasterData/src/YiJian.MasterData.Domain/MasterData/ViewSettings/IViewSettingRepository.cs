using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData.ViewSettings;


/// <summary>
/// 视图配置 仓储接口
/// </summary>  
public interface IViewSettingRepository : IRepository<ViewSetting, int>
{
    /// <summary>
    /// 获取列表记录
    /// </summary>    
    /// <param name="view">视图名称</param>
    Task<List<ViewSetting>> GetListAsync(string view);
    /// <summary>
    /// 重置
    /// </summary>
    /// <param name="view">视图名称</param>
    /// <returns></returns>
    Task ResetAsync(string view);
}
