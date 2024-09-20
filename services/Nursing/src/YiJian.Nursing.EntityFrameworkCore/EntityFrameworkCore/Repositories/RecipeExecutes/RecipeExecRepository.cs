using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class RecipeExecRepository : NursingRepositoryBase<RecipeExec, Guid>, IRecipeExecRepository
    {
        public RecipeExecRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
}
