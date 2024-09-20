using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Regions;
using YiJian.MasterData.Separations.Contracts;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Regions;

/// <summary>
/// 地区存储实现
/// </summary>
public class RegionRepository : EfCoreRepository<MasterDataDbContext, Region, Guid>, IRegionRepository
{
    public RegionRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
        
    }
}
