using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Regions;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Regions;

/// <summary>
/// 描述：地区仓储
/// 创建人： yangkai
/// 创建时间：2023/2/10 17:47:55
/// </summary>
public class AreaRepository : EfCoreRepository<MasterDataDbContext, Area, Guid>, IAreaRepository
{
    public AreaRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}
