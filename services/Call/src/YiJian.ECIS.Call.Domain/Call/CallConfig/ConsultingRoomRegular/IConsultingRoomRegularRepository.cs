using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【诊室固定】仓储接口
    /// </summary>
    public interface IConsultingRoomRegularRepository : IRepository<ConsultingRoomRegular, Guid>
    {
        Task<long> GetCountAsync();

        Task<List<ConsultingRoomRegular>> GetListAsync(
            string sorting = null);

        Task<List<ConsultingRoomRegular>> GetPagedListAsync(
            int skipCount = 0,
            int maxResultCount = int.MaxValue,
            string sorting = null);
    }
}
