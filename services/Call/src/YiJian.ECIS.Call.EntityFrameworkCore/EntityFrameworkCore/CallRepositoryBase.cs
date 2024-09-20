namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    using Volo.Abp.Domain.Entities;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;

    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class CallRepositoryBase<TEntity, TKey> : EfCoreRepository<CallDbContext, TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected CallRepositoryBase(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        // 在此添加自定义仓储的通用逻辑

    }
    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class CallRepositoryBase<TEntity> : CallRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        protected CallRepositoryBase(IDbContextProvider<CallDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        // --------------------------------------注意-------------------------------------------------------------
        // 一定不要在此添加自定义仓储的通用逻辑 在上面泛型基类中添加
        // --------------------------------------注意-------------------------------------------------------------
    }
}
