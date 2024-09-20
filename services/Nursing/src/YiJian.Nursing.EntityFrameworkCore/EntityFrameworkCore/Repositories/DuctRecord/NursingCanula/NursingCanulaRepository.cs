using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class NursingCanulaRepository : NursingRepositoryBase<NursingCanula, Guid>, INursingCanulaRepository
    {
        public NursingCanulaRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {

        }
    }
}