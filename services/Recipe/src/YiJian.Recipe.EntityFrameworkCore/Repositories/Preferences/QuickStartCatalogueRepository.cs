using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Repositories.Preferences
{
    /// <summary>
    /// 快速开嘱
    /// </summary>
    public class QuickStartCatalogueRepository : EfCoreRepository<RecipeDbContext, QuickStartCatalogue, Guid>, IQuickStartCatalogueRepository
    {
        /// <summary>
        /// 快速开嘱
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public QuickStartCatalogueRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
