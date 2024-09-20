using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.Labs;


/// <summary>
/// 检验标本 API Interface
/// </summary>   
public interface ILabSpecimenAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(LabSpecimenCreation input);

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(LabSpecimenUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<LabSpecimenData> GetAsync(int id);

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
    Task<ListResultDto<LabSpecimenData>> GetListAsync(
        string filter = null,
        string sorting = null);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<LabSpecimenData>> GetPagedListAsync(GetLabSpecimenPagedInput input);

}
