namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    public interface IDoctorPatientStatisticRepository : IRepository<DoctorPatientStatistic, Guid>
    {
        Task<long> GetCountAsync(string filter = null);

        Task<List<DoctorPatientStatistic>> GetListAsync(
            string sorting = null,
            string filter = null);

        Task<List<DoctorPatientStatistic>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null);        
    }
}