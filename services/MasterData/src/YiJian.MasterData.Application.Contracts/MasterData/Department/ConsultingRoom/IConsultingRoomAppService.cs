using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.MasterData;

public interface IConsultingRoomAppService : IApplicationService
{
    Task<ConsultingRoomData> GetAsync(Guid id);
    Task<ConsultingRoomData> CreateAsync(ConsultingRoomCreation input);
    Task<ConsultingRoomData> UpdateAsync(Guid id, ConsultingRoomUpdate input);
    Task<Guid> DeleteAsync(Guid id);
    Task<List<ConsultingRoomData>> GetListAsync();
}
