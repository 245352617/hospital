using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call.Domain.CallCenter;

namespace YiJian.ECIS.Call.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 【叫号信息】仓储
    /// </summary>
    public class CallInfoRepository : CallRepositoryBase<CallInfo, Guid>, ICallInfoRepository
    {
        public CallInfoRepository(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}
