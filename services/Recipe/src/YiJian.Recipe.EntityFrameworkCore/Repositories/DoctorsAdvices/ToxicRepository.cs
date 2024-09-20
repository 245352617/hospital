using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 药理等级
    /// </summary>
    public class ToxicRepository : EfCoreRepository<RecipeDbContext, Toxic, Guid>, IToxicRepository
    {
        /// <summary>
        /// 药理等级
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public ToxicRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
