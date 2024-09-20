using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call.Domain.CallConfig;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 【诊室固定】仓储
    /// </summary>
    public class ConsultingRoomRegularRepository : CallRepositoryBase<ConsultingRoomRegular, Guid>, IConsultingRoomRegularRepository
    {
        public ConsultingRoomRegularRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<long> GetCountAsync()
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .LongCountAsync();
        }

        public async Task<List<ConsultingRoomRegular>> GetListAsync(string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(ConsultingRoomRegular.Id) : sorting)
                .ToListAsync();
        }

        public async Task<List<ConsultingRoomRegular>> GetPagedListAsync(int skipCount = 0, int maxResultCount = int.MaxValue, string sorting = null)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .OrderBy(sorting.IsNullOrWhiteSpace() ? nameof(ConsultingRoomRegular.Id) : sorting)
                .PageBy(skipCount, maxResultCount)
                .ToListAsync();
        }
    }
}
