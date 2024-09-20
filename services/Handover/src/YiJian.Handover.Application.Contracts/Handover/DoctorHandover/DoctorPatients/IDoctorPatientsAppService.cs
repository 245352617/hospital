namespace YiJian.Handover
{
    using System;
    using System.Threading.Tasks;
    using Volo.Abp.Application.Dtos;
    using Volo.Abp.Application.Services;

    public interface IDoctorPatientsAppService : IApplicationService
    {
        /// <summary>
        /// 保存交班患者
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<Guid> SaveHandoverPatientAsync(DoctorPatientsUpdate dto);

        Task<DoctorPatientsData> GetHandoverPatientAsync(Guid id);

        Task DeleteHandoverPatientAsync(Guid id);
    }
}