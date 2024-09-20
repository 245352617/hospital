using SamJan.MicroService.PreHospital.Core;
using System;
using Volo.Abp.EntityFrameworkCore;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ChangeRecordRepository : BaseRepository<PreHospitalTriageDbContext, PatientInfoChangeRecord, Guid>,
        IChangeRecordRepository
    {
        public ChangeRecordRepository(IDbContextProvider<PreHospitalTriageDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
