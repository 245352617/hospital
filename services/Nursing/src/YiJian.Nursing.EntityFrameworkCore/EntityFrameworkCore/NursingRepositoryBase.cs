using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Nursing.EntityFrameworkCore
{
    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class NursingRepositoryBase<TEntity, TKey> : EfCoreRepository<NursingDbContext, TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected NursingRepositoryBase(IDbContextProvider<NursingDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        // 在此添加自定义仓储的通用逻辑

    }
}
