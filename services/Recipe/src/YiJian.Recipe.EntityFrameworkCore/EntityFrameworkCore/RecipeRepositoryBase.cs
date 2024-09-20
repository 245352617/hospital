namespace YiJian.Recipe.EntityFrameworkCore
{
    using Volo.Abp.Domain.Entities;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;

    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class RecipeRepositoryBase<TEntity, TKey> : EfCoreRepository<RecipeDbContext, TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected RecipeRepositoryBase(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

    }
    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class RecipeRepositoryBase<TEntity> : RecipeRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        protected RecipeRepositoryBase(IDbContextProvider<RecipeDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
