using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.MasterData.Regions;

/// <summary>
/// 地区
/// </summary>
public interface IRegionAppService : IApplicationService
{
    /// <summary>
    /// 获取区域
    /// </summary>
    /// <param name="code"></param>
    /// <param name="regionType"></param>
    /// <returns></returns>
    Task<List<RegionDto>> GetAsync(string code = "", ERegionType regionType = ERegionType.Province);

    /// <summary>
    /// 模糊匹配查询地区
    /// </summary>
    /// <param name="region"></param>
    /// <returns></returns>
    Task<List<RegionDto>> GetRegionsAsync(string region);
}