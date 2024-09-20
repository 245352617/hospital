using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.ECIS.Call.Domain.CallCenter
{
    /// <summary>
    /// 【叫号患者信息】仓储
    /// </summary>
    public interface ICallInfoRepository : IRepository<CallInfo, Guid>
    {
    }
}
