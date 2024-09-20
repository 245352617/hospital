using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call.CallCenter;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 叫号记录
    /// </summary>
    public class CallingRecordRepository : CallRepositoryBase<CallingRecord, Guid>, ICallingRecordRepository
    {
        public CallingRecordRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="skipCount"></param>
        /// <param name="maxResultCount"></param>
        /// <param name="triageDept">科室代码</param>
        /// <returns></returns>
        public async Task<(List<CallingRecord>, long)> GetPagedListAsync(int skipCount = 0, int maxResultCount = 20, string triageDept = null)
        {
            var query = (await GetDbSetAsync())
                .AsNoTracking()
                .Include(x => x.CallInfo)
                .WhereIf(!string.IsNullOrEmpty(triageDept), x => x.CallInfo.TriageDept == triageDept)  // 科室
                .OrderByDescending(x => x.CreationTime);  // 按时间倒序排序
            var list = await query.PageBy(skipCount, maxResultCount)
                .ToListAsync();
            var count = await query.LongCountAsync();

            return (list, count);
        }
    }
}
