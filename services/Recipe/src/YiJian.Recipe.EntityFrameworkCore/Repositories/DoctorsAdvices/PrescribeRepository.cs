using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 药方
    /// </summary>
    public class PrescribeRepository : EfCoreRepository<RecipeDbContext, Prescribe, Guid>, IPrescribeRepository
    {
        /// <summary>
        /// 药方
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PrescribeRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
