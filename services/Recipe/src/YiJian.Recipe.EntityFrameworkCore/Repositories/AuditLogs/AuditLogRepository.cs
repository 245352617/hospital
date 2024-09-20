using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.AuditLogs.Contracts;
using YiJian.AuditLogs.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.AuditLogs
{
    /// <summary>
    /// HIS接口审计日志
    /// </summary>
    public class AuditLogRepository : EfCoreRepository<RecipeDbContext, AuditLog, int>, IAuditLogRepository
    {

        /// <summary>
        /// HIS接口审计日志
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public AuditLogRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 写入HIS请求审计日志
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task WriterLogAsync(AuditLog entity)
        {
            var db = await GetDbContextAsync();
            //不需要告诉前端，只是做一个审计日志
            _ = await db.AuditLogs.AddAsync(entity);
            await db.SaveChangesAsync();
        }

    }
}
