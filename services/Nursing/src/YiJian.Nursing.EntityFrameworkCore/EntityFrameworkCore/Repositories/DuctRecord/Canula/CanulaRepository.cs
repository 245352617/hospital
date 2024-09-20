using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class CanulaRepository : NursingRepositoryBase<Canula, Guid>, ICanulaRepository
    {
        public CanulaRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}