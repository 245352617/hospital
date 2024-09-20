using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipe.Preferences.Contracts;
using YiJian.Recipes.Preferences.Entities;

namespace YiJian.Recipe.Repositories.Preferences
{
    /// <summary>
    /// 快速开嘱药品
    /// </summary>
    public class QuickStartMedicineRepository : EfCoreRepository<RecipeDbContext, QuickStartMedicine, Guid>, IQuickStartMedicineRepository
    {
        /// <summary>
        /// 快速开嘱
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public QuickStartMedicineRepository(IDbContextProvider<RecipeDbContext> dbContextProvider)
          : base(dbContextProvider)
        {

        }
    }
}
