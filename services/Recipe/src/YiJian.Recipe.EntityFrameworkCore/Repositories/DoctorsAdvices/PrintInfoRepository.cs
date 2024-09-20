using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 医嘱打印信息
    /// </summary>
    public class PrintInfoRepository : EfCoreRepository<RecipeDbContext, PrintInfo, Guid>, IPrintInfoRepository
    {
        /// <summary>
        /// 医嘱打印信息
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PrintInfoRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}