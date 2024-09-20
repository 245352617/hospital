namespace YiJian.Health.Report.EntityFrameworkCore
{
    using Volo.Abp.Domain.Entities;
    using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
    using Volo.Abp.EntityFrameworkCore;

    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public abstract class ReportRepositoryBase<TEntity, TKey> : EfCoreRepository<ReportDbContext, TEntity, TKey> where TEntity : class, IEntity<TKey>
    {
        protected ReportRepositoryBase(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }  
    }
    /// <summary>
    /// 自定义仓库基类
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class ReportRepositoryBase<TEntity> : ReportRepositoryBase<TEntity, int> where TEntity : class, IEntity<int>
    {
        protected ReportRepositoryBase(IDbContextProvider<ReportDbContext> dbContextProvider) : base(dbContextProvider)
        {
        } 
    }
}
