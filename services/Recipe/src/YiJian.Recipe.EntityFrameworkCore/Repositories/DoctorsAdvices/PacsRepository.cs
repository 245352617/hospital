using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 检查项
    /// </summary>
    public class PacsRepository : EfCoreRepository<RecipeDbContext, Pacs, Guid>, IPacsRepository
    {
        /// <summary>
        /// 检查项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PacsRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
