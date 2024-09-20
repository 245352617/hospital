using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using YiJian.MasterData.Medicines;

namespace YiJian.MasterData.External.LongGang.Medicines;

public interface IHisMedicineAppService : IApplicationService
{

    Task<PagedResultDto<MedicineData>> GetHisPagedListAsync(GetMedicinePagedInput input);
     
}
