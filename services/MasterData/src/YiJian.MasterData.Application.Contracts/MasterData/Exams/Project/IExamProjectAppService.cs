using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Exams;

/// <summary>
/// 检查申请项目 API Interface
/// </summary>   
public interface IExamProjectAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(ExamProjectCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> UpdateAsync(ExamProjectUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ExamProjectData> GetAsync(int id);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task DeleteAsync(int id);

    Task<ExamProjectData> GetDetailsAsync(string code,PlatformType type = PlatformType.EmergencyTreatment);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<ExamProjectData>> GetListAsync(
        string cateCode, string filter = null, PlatformType platformType = PlatformType.All);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<ExamProjectData>> GetPagedListAsync(GetExamProjectInput input);
}