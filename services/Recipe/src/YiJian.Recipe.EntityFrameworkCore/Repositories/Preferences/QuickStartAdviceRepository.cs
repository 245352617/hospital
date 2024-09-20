using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Repositories.Preferences
{
    /// <summary>
    /// 快速开嘱医嘱
    /// </summary>
    public class QuickStartAdviceRepository : EfCoreRepository<RecipeDbContext, QuickStartAdvice, Guid>, IQuickStartAdviceRepository
    {
        /// <summary>
        /// 快速开嘱目录
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public QuickStartAdviceRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
