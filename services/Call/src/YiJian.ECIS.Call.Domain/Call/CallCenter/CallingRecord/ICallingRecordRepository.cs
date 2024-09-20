using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.CallCenter
{
    /// <summary>
    /// 叫号记录
    /// </summary>
    public interface ICallingRecordRepository : IRepository<CallingRecord, Guid>
    {
        Task<(List<CallingRecord>, long)> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = 20,
            string triageDept = null);
    }
}
