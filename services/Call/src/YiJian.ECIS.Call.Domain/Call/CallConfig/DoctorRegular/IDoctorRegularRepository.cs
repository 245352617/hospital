using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【医生变动】仓储接口
    /// </summary>
    public interface IDoctorRegularRepository : IRepository<DoctorRegular, Guid>
    {
        Task<long> GetCountAsync();

        Task<List<DoctorRegular>> GetListAsync(
            string sorting = null);

        Task<List<DoctorRegular>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string sorting = null);
    }
}
