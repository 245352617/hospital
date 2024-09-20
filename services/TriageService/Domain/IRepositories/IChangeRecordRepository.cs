using SamJan.MicroService.PreHospital.Core;
using System;
using Volo.Abp.Domain.Repositories;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public interface IChangeRecordRepository : IRepository<PatientInfoChangeRecord, Guid>, IBaseRepository<PatientInfoChangeRecord, Guid>
    {
    }
}
