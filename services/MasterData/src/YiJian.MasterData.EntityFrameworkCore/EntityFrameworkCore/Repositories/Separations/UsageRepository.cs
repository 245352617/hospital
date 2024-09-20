using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Separations.Contracts;
using YiJian.MasterData.Separations.Entities;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Separations;

/// <summary>
/// 分方-途经存储
/// </summary>
public class UsageRepository : EfCoreRepository<MasterDataDbContext, Usage, Guid>, IUsageRepository
{
    /// <summary>
    /// 途经存储
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public UsageRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    { 
    }
}
