using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;


/// <summary>
/// 检查目录 API Interface
/// </summary>   
public interface IExamCatalogAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(ExamCatalogCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(ExamCatalogUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<ExamCatalogData> GetAsync(int id);

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
    Task<ListResultDto<ExamCatalogData>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<ExamCatalogFirstNodeData>> GetListV2Async(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    Task<ListResultDto<ExamCatalogFirstNodeData>> GetListV3Async(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<ExamCatalogData>> GetPagedListAsync(GetExamCatalogPagedInput input);

}
