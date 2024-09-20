namespace YiJian.ECIS.IMService.EntityFrameworkCore
{
    using Volo.Abp.Domain.Entities;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;

    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class IMServiceRepositoryBase<TEntity, TKey> : EfCoreRepository<IMServiceDbContext, TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected IMServiceRepositoryBase(IDbContextProvider<IMServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        // 在此添加自定义仓储的通用逻辑

    }
    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class IMServiceRepositoryBase<TEntity> : IMServiceRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        protected IMServiceRepositoryBase(IDbContextProvider<IMServiceDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        // --------------------------------------注意-------------------------------------------------------------
        // 一定不要在此添加自定义仓储的通用逻辑 在上面泛型基类中添加
        // --------------------------------------注意-------------------------------------------------------------
    }
}
