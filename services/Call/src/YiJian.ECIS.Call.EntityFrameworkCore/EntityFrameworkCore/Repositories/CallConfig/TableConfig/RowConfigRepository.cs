using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Linq;
using YiJian.ECIS.Call.CallConfig;
using YiJian.ECIS.Call.Domain.CallConfig;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories.CallConfig.TableConfig
{
    /// <summary>
    /// 列表配置
    /// </summary>
    public class RowConfigRepository : EfCoreRepository<CallDbContext, RowConfig>, IRowConfigRepository
    {
        public RowConfigRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
