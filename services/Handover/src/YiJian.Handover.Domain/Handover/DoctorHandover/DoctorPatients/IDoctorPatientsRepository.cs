namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    public interface IDoctorPatientsRepository : IRepository<DoctorPatients, Guid>
    {
        Task<long> GetCountAsync(string filter = null);

        Task<List<DoctorPatients>> GetListAsync(
            string sorting = null,
            string filter = null);

        Task<List<DoctorPatients>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null);        
    }
}