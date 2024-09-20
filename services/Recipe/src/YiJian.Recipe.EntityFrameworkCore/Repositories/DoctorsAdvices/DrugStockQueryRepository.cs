using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Contracts;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.DoctorsAdvices
{
    /// <summary>
    /// 
    /// </summary>
    public class DrugStockQueryRepository : EfCoreRepository<RecipeDbContext, DrugStockQuery, int>, IDrugStockQueryRepository
    {
        public DrugStockQueryRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
