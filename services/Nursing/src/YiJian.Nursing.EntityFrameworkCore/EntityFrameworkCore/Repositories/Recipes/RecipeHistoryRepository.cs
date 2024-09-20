using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Nursing.Recipes;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 医嘱操作历史 仓储实现
    /// </summary> 
    public class RecipeHistoryRepository : NursingRepositoryBase<RecipeHistory, Guid>, IRecipeHistoryRepository
    {
        #region constructor
        public RecipeHistoryRepository(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        #endregion

    }
}

