using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbPrimaryType = System.String;

namespace YiJian.ECIS.Core
{
    public interface IRepository<T> : IRepository
        where T : class
    {
        DbSet<T> DbSet { get; }

        Task<IEnumerable<T>> ExecuteQueryAsync(string rawSql, params object[] pars);
        IEnumerable<T> ExecuteQuery(string rawSql, params object[] pars);

        Task<T> GetAsync(Expression<Func<T, bool>> condition, Expression<Func<T, object>> sort = null, bool isDesc = false, IRepoOptions options = null);

        T Get(Expression<Func<T, bool>> condition, IRepoOptions options = null);

        T GetById(DbPrimaryType id, IRepoOptions options = null);

        Task<T> GetByIdAsync(DbPrimaryType id, IRepoOptions options = null);
        /// <summary>
        /// 新添实体
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="storagePartition"></param>
        /// <returns></returns>
        Task<T> CreateAsync(T entity, IRepoOptions options = null);

        Task CreateRangeAsync(IEnumerable<T> entity, IRepoOptions options = null);

        T Create(T entity, IRepoOptions options = null);
        /// <summary>
        /// 修改实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="storagePartition"></param>
        /// <returns></returns>
        Task<T> ModifyAsync(T entity, IRepoOptions options = null);

        Task<T> SetValueAsync(T src, T dst, IRepoOptions options = null);

        /// <summary>
        /// 批量修改数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task ModifyRangeAsync(IEnumerable<T> entity, IRepoOptions options = null);

        /// <summary>
        /// 根据条件，获取列表
        /// 也可用于分页
        /// </summary>
        /// <param name="pageable"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<Pageable<T>> ListAsync(Pageable<T> pageable, IRepoOptions options = null);

        Pageable<T> List(Pageable<T> pageable, IRepoOptions options = null);

        Task<IList<T>> ListAsync(Expression<Func<T, bool>> where, Expression<Func<T, object>> sort = null, bool isDesc = false, IRepoOptions options = null);

        IList<T> List(Expression<Func<T, bool>> where, Expression<Func<T, object>> sort = null, bool isDesc = false, IEnumerable<Expression<Func<T, object>>> Includes = null, IRepoOptions options = null);

        Task<Pageable<TValue>> SelectAsync<TValue>(Pageable<T> pageable, Expression<Func<T, TValue>> what, bool distinct = true, IRepoOptions options = null);

        T Modify(T entity, IRepoOptions options = null);

        T DeleteById(DbPrimaryType id, IRepoOptions options = null);
        /// <summary>
        /// 根据Id删除实体数据
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<T> DeleteByIdAsync(DbPrimaryType id, IRepoOptions options = null);

        /// <summary>
        /// 删除实体数据
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        Task<T> DeleteAsync(T entity, IRepoOptions options = null);
        Task<IList<T>> DeleteAsync(IList<T> entities, IRepoOptions options = null);
        T Delete(T entity, IRepoOptions options = null);


    }
}
