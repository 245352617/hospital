using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱审计
    /// </summary>
    public class DoctorsAdviceAuditRepository : EfCoreRepository<RecipeDbContext, DoctorsAdviceAudit, Guid>, IDoctorsAdviceAuditRepository
    {
        /// <summary>
        /// 诊疗项
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public DoctorsAdviceAuditRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
