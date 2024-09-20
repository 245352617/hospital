using Volo.Abp.EntityFrameworkCore;
using YiJian.MasterData.EntityFrameworkCore;
using YiJian.MasterData.MasterData.Doctors;

namespace YiJian.MasterData;

public class DoctorRepository : MasterDataRepositoryBase<Doctor, int>, IDoctorRepository
{
    public DoctorRepository(IDbContextProvider<MasterDataDbContext> dbContextProvider) : base(dbContextProvider)
    {

    }
}