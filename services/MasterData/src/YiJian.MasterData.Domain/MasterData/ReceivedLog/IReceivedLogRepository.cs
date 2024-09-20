using System;
using Volo.Abp.Domain.Repositories;

namespace YiJian.MasterData;

public  interface IReceivedLogRepository : IRepository<ReceivedLog, Guid>
{
}
