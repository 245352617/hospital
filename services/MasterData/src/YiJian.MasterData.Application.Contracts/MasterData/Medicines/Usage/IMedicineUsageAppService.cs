using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 药品用法字典 API Interface
/// </summary>   
public interface IMedicineUsageAppService : IApplicationService
{
    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> CreateAsync(MedicineUsageCreation input);

    /// <summary>
    /// 修改
    /// </summary>=
    /// <param name="input"></param>
    /// <returns></returns>
    Task UpdateAsync(MedicineUsageUpdate input);

    /// <summary>
    /// 获取
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<MedicineUsageData> GetAsync(int id);

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
    Task<ListResultDto<MedicineUsageData>> GetListAsync(
        string filter = null,
        string sorting = null, int code = -1);

    /// <summary>
    /// 获取分页记录
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<PagedResultDto<MedicineUsageData>> GetPagedListAsync(GetMedicineUsagePagedInput input);
}