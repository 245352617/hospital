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
    public class LisRepository : EfCoreRepository<RecipeDbContext, Lis, Guid>, ILisRepository
    {
        /// <summary>
        /// 检验项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public LisRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
