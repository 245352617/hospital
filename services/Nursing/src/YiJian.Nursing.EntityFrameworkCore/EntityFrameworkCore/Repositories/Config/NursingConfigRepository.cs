using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Config;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class NursingConfigRepository : NursingRepositoryBase<NursingConfig, Guid>, INursingConfigRepository
    {
        public NursingConfigRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
