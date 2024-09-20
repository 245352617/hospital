using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.DoctorsAdvices.Entities;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Contracts;

namespace YiJian.Recipe.Repositories.DoctorsAdvices
{
    /// <summary>
    /// 回调HIS返回的结果
    /// </summary>
    public class MedDetailResultRepository : EfCoreRepository<RecipeDbContext, MedDetailResult, Guid>, IMedDetailResultRepository
    {
        /// <summary>
        /// 回调HIS返回的结果
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public MedDetailResultRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
