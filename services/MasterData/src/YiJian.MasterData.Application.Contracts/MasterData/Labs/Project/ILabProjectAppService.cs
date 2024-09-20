using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData;

/// <summary>
/// 检验项目 API Interface
/// </summary>   
public interface ILabProjectAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(LabProjectCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(LabProjectUpdate input);

    Task<LabProjectData> GetDetailsAsync(string code,PlatformType type = PlatformType.EmergencyTreatment);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LabProjectData> GetAsync(int id);

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
    Task<ListResultDto<LabProjectData>> GetListAsync(string cateCode,
        string filter, PlatformType platformType);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<LabProjectData>> GetPagedListAsync(GetLabProjectPagedInput input);
}