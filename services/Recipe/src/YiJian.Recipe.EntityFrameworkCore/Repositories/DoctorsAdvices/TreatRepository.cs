using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 诊疗项
    /// </summary>
    public class TreatRepository : EfCoreRepository<RecipeDbContext, Treat, Guid>, ITreatRepository
    {
        /// <summary>
        /// 诊疗项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public TreatRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
