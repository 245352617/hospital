using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.DictionariesType;
using YiJian.MasterData.EntityFrameworkCore;

namespace YiJian.MasterData;

public class EntrustRepository : MasterDataRepositoryBase<Entrust, Guid>,
    IEntrustRepository
{
    public EntrustRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }
}