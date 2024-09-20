using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.MasterData;

namespace YiJian.MasterData.Treats;

/// <summary>
/// 诊疗项目字典 API Interface
/// </summary>   
public interface ITreatAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(TreatCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(TreatUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TreatData> GetAsync(int id);

    Task<TreatData> GetDetailsAsync(string code,PlatformType type = PlatformType.EmergencyTreatment);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(int id);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<TreatData>> GetListAsync(
        string filter = null,
        string sorting = null, PlatformType platformType = PlatformType.All,string categoryCode=null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<TreatData>> GetPagedListAsync(GetTreatPagedInput input);

    /// <summary>
    /// 修改默认频次
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> UpdateFrequencyAsync(UpdateFrequencyDto input);
}