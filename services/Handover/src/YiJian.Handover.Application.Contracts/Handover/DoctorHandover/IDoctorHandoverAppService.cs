using System.Collections.Generic;

namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;

    public interface IDoctorHandoverAppService : IApplicationService
    {
        Task<Guid> CreateDoctorHandoverAsync(DoctorHandoverCreation input);

        Task UpdateDoctorHandoverAsync(DoctorHandoverUpdate input);

        Task<DoctorHandoverData> GetDoctorHandoverAsync(string doctorCode, Guid shiftId, string date);

        Task<PagedResultDto<DoctorHandoverStatisticDataList>> GetHistoryListAsync(GetDoctorHandoverInput input);

        Task<DoctorHandoverStatisticDataList> GetDetailsAsync(
            string date,
            Guid shiftId);
    }
}