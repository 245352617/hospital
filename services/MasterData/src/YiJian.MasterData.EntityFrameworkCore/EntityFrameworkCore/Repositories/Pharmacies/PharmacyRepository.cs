using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.Pharmacies.Contracts;
using YiJian.MasterData.Pharmacies.Entities;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories.Pharmacies;

/// <summary>
/// 药房配置
/// </summary>
public class PharmacyRepository : EfCoreRepository<MasterDataDbContext, Pharmacy, Guid>, IPharmacyRepository
{
    /// <summary>
    /// 药房配置
    /// </summary>
    /// <param name="dbContextProvider"></param>
    public PharmacyRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }
}
