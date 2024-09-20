using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Contracts;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;

namespace YiJian.Recipe.Repositories.DoctorsAdvices
{
    public class MedicalTechnologyMapRepository : EfCoreRepository<RecipeDbContext, MedicalTechnologyMap, long>, IMedicalTechnologyMapRepository
    {
        public MedicalTechnologyMapRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
         : base(dbContextProvider)
        {

        }
    }
}
