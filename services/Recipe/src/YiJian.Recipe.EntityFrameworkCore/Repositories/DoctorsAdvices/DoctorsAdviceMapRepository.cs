using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱映射表
    /// </summary>
    public class DoctorsAdviceMapRepository : EfCoreRepository<RecipeDbContext, DoctorsAdviceMap, long>, IDoctorsAdviceMapRepository
    {
        /// <summary>
        /// 医嘱映射表
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DoctorsAdviceMapRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
