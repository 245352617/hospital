using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Contracts;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipe.Repositories.DoctorsAdvices
{
    /// <summary>
    /// 描    述:检查病理小项序号仓储类
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/29 9:37:11
    /// </summary>
    public class PacsPathologyItemNoRepository : EfCoreRepository<RecipeDbContext, PacsPathologyItemNo, int>, IPacsPathologyItemNoRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PacsPathologyItemNoRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
