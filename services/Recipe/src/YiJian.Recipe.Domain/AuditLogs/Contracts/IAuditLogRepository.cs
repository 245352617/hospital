using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.AuditLogs.Entities;

namespace YiJian.AuditLogs.Contracts
{
    /// <summary>
    /// HIS接口审计日志
    /// </summary>
    public interface IAuditLogRepository : IRepository<AuditLog, int>
    {
        /// <summary>
        /// 写入HIS请求审计日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Task WriterLogAsync(AuditLog entity);

    }
}
