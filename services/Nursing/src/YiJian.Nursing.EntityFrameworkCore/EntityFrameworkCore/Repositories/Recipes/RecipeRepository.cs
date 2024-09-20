using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 医嘱 仓储实现
    /// </summary> 
    public class RecipeRepository : NursingRepositoryBase<Recipe, Guid>, IRecipeRepository
    {
        public RecipeRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}

