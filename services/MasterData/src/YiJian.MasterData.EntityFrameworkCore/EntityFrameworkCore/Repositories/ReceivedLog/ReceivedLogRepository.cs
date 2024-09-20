using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;

public class ReceivedLogRepository: EfCoreRepository<MasterDataDbContext, ReceivedLog, Guid>,IReceivedLogRepository
{
    /// <summary>
    /// 药房配置
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public ReceivedLogRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
{

}
}
