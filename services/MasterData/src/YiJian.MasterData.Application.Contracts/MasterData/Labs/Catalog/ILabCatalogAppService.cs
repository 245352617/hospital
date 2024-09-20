using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.Labs;


/// <summary>
/// 检验目录 API Interface
/// </summary>   
public interface ILabCatalogAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(LabCatalogCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(LabCatalogUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LabCatalogData> GetAsync(int id);

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
    Task<ListResultDto<LabCatalogData>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<LabCatalogData>> GetPagedListAsync(GetLabCatalogPagedInput input);

}
