using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class CanulaDynamicRepository : NursingRepositoryBase<CanulaDynamic, Guid>, ICanulaDynamicRepository
    {
        public CanulaDynamicRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}