using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【排队号规则】仓储接口
    /// </summary>
    public interface ISerialNoRuleRepository : IRepository<SerialNoRule, int>
    {
        /// <summary>
        /// 获取指定部门的排队号规则
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <returns></returns>
        Task<SerialNoRule> GetSerialNoRuleAsync(Guid departmentId);
    }
}
