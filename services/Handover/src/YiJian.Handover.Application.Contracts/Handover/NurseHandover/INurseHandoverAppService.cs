using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;

    /// <summary>
    /// 护士交班 API Interface
    /// </summary>   
    public interface INurseHandoverAppService : IApplicationService
    {
        Task<Guid> SaveNurseHandoverAsync(NurseHandoverCreation input);

        Task<List<NurseHandoverData>> GetNurseHandoverAsync(Guid shiftId,
            string handoverDate, string creationCode);

        Task<PagedResultDto<NurseHandoverDataList>> GetListAsync(GetNurseHandoverPagedInput input);

        Task<string> ImportUpDeviceAsync(GetNurseHandoverInput input);

        Task<StatisticsData> GetDetailsAsync(string date,
            Guid shiftId);
    }
}