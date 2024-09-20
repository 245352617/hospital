using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call.Domain.CallConfig;
using System.Linq.Dynamic.Core;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 【排队号规则】仓储
    /// </summary>
    public class SerialNoRuleRepository : CallRepositoryBase<SerialNoRule, int>, ISerialNoRuleRepository
    {
        public SerialNoRuleRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        /// <summary>
        /// 获取指定部门的排队号规则
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns></returns>
        public async Task<SerialNoRule> GetSerialNoRuleAsync(Guid departmentId)
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .Where(d => d.DepartmentId == departmentId)
                .OrderBy(nameof(SerialNoRule.CreationTime))
                .FirstOrDefaultAsync();
        }
    }
}
