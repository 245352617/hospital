using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.Sequences.Entities;

namespace YiJian.Sequences.Contracts
{
    /// <summary>
    /// 我的系列号管理器仓储接口
    /// </summary>
    public interface IMySequenceRepository : IRepository<MySequence, Guid>
    {
        /// <summary>
        /// 确认表名和字段名获取最新的系列号
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="filedName">字段名</param>
        /// <returns></returns>
        public Task<int> GetSequenceAsync(string tableName, string filedName);
    }
}
