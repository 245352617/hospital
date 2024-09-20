using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using YiJian.Recipe.EntityFrameworkCore;
using YiJian.Recipes.DoctorsAdvices.Entities;

namespace YiJian.Recipes.DoctorsAdvices.Contracts
{
    /// <summary>
    /// 描    述:检查病理小项仓储类
    /// 创 建 人:杨凯
    /// 创建时间:2023/11/24 14:37:30
    /// </summary>
    public class PacsPathologyItemRepository : EfCoreRepository<RecipeDbContext, PacsPathologyItem, Guid>, IPacsPathologyItemRepository
    {
        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public PacsPathologyItemRepository(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
