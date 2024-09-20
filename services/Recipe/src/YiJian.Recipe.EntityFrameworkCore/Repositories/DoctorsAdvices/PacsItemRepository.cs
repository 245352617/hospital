using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检查小项
    /// </summary>
    public class PacsItemRepository : EfCoreRepository<RecipeDbContext, PacsItem, Guid>, IPacsItemRepository
    {
        /// <summary>
        /// 检查小项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PacsItemRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
