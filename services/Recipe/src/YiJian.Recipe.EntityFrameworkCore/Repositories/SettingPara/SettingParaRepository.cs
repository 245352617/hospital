using System;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Recipe.EntityFrameworkCore.Repositories
{
    public class SettingParaRepository : RecipeRepositoryBase<SettingPara, Guid>,
        ISettingParaRepository
    {
        public SettingParaRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}