using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.MasterData.EntityFrameworkCore.Repositories;


/// <summary>
/// 诊疗分组字典 仓储实现
/// </summary> 
public class TreatGroupRepository : MasterDataRepositoryBase<TreatGroup, Guid>, ITreatGroupRepository
{
    public TreatGroupRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}