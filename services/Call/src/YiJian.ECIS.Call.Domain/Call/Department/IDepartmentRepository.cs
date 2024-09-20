namespace YiJian.ECIS.Call.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Volo.Abp.Domain.Repositories;

    public interface IDepartmentRepository : IRepository<Department, Guid>
    {
        Task<long> GetCountAsync(string filter = null);

        public Task<List<Department>> GetListAsync(string sorting = null, string filter = null, bool includeDetails = false);

        public Task<List<Department>> GetPagedListAsync(int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null, string filter = null, bool includeDetails = false);

        Task<Department> GetByCodeAsync(string code);

    }
}
