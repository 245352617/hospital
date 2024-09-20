using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检验项
    /// </summary>
    public class LisItemRepository : EfCoreRepository<RecipeDbContext, LisItem, Guid>, ILisItemRepository
    {
        /// <summary>
        /// 检验项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public LisItemRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }

    }
}
