namespace YiJian.Handover
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    public interface IDoctorHandoverRepository : IRepository<DoctorHandover, Guid>
    {
        Task<long> GetCountAsync(string startDate = null,
            string endDate = null);

        Task<List<DoctorHandover>> GetListAsync(
            string sorting = null,
            string filter = null, string startDate = null, string endDate = null);

        Task<List<DoctorHandover>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string filter = null, string startDate = null, string endDate = null);
    }
}