using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.MasterData.MasterData;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 API Interface
/// </summary>   
public interface IMedicineAppService : IApplicationService
{
    Task<int> CreateAsync(MedicineCreation input);

    Task<int> UpdateAsync(MedicineUpdate input);

    Task<MedicineData> GetAsync(int id);
    Task<bool> CheckRationalUseAsync(string code);
    Task DeleteAsync(int id);
    Task<MedicineData> GetDetailsAsync(string code,PlatformType type = PlatformType.EmergencyTreatment);
    Task<string[]> GetCategoriesAsync();

    Task<PagedResultDto<MedicineData>> GetPagedListAsync(GetMedicinePagedInput input);
    /// <summary>
    /// 同步药品药理信息
    /// </summary>
    /// <returns></returns> 
    Task SyncAllToxicAsync();

    /// <summary>
    /// 修改药品的默认频次，用法
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    Task<int> UpdateFrequencyUsageAsync(UpdateFrequencyUsageDto input);
}