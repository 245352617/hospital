namespace YiJian.MasterData.EntityFrameworkCore;

using Volo.Abp.EntityFrameworkCore;
public class BedRepository : MasterDataRepositoryBase<Bed>,IBedRepository
{
    public BedRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }
}
