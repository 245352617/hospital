using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.RecipeExecutes;
using YiJian.Nursing.RecipeExecutes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    public class RecipeExecHistoryRepository : NursingRepositoryBase<RecipeExecHistory, Guid>, IRecipeExecHistoryRepository
    {
        public RecipeExecHistoryRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
}
