using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.ViewSettings;

/// <summary>
/// 视图配置 API Interface
/// </summary>   
public interface IViewSettingAppService : IApplicationService
{
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(ViewSettingUpdate[] input);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    Task<ListResultDto<ViewSettingData>> GetListAsync(string view);

    /// <summary>
    /// 重置
    /// </summary>
    /// <param name="view"></param>
    /// <returns></returns>
    Task<ListResultDto<ViewSettingData>> ResetAsync(string view);
}

