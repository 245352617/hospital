using System;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes;

namespace YiJian.Recipe.Repositories
{
    /// <summary>
    /// 新冠rna检测申请
    /// </summary>
    public class NovelCoronavirusRnaRepository : RecipeRepositoryBase<NovelCoronavirusRna, Guid>,
        INovelCoronavirusRnaRepository
    {
        #region constructor

        public NovelCoronavirusRnaRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(
            dbContextProvider)
        {
        }

        #endregion
    }
}