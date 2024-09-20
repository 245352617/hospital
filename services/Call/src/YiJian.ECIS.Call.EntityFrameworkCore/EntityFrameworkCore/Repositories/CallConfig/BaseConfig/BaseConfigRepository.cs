using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.ShareModel.Exceptions;
using System.Linq.Dynamic.Core;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 基础设置 仓储
    /// </summary>
    public class BaseConfigRepository : CallRepositoryBase<BaseConfig, int>, IBaseConfigRepository
    {
        public BaseConfigRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }

        /// <summary>
        /// 获取最新的配置信息
        /// </summary>
        /// <returns></returns>
        public async Task<BaseConfig> GetLastConfigAsync()
        {
            return await (await GetDbSetAsync())
                .AsNoTracking()
                .OrderByDescending(x => x.CreationTime)
                .FirstOrDefaultAsync();
        }
    }
}
