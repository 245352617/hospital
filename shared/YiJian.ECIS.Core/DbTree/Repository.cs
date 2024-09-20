using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DbPrimaryType = System.String;

namespace YiJian.ECIS.Core
{
    /// <summary>
    /// 仓储基类
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    public class Repository<TEntity> : IRepository<TEntity>
        where TEntity : class
    {

        public Repository(DbContext context)
        {
            this.DbContext = context;
            this.DbSet = this.DbContext.Set<TEntity>();
        }
        public DbSet<TEntity> DbSet { get; set; }
        /// <summary>
        /// 数据上下文
        /// </summary>
        public DbContext DbContext { get; private set; }
        /// <summary>
        /// 异步 保存操作
        /// </summary>
        /// <returns></returns>
        public async Task<int> SaveChangesAsync()
        {
            return await this.DbContext.SaveChangesAsync();
        }
        /// <summary>
        /// 保存操作
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return this.DbContext.SaveChanges();
        }
        /// <summary>
        /// 部分更新
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="fieldNames">更新列集合</param>
        /// <returns></returns>
        public int Update(TEntity entity, List<string> fieldNames)
        {

            if (fieldNames != null && fieldNames.Count > 0)
            {
                DbContext.Set<TEntity>().Attach(entity);
                foreach (var item in fieldNames)
                {
                    DbContext.Entry<TEntity>(entity).Property(item).IsModified = true;
                }
            }
            else
            {
                DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
            }

            return DbContext.SaveChanges();

        }
        /// <summary>
        /// 部分更新
        /// </summary>
        /// <param name="list">实体集合</param>
        /// <param name="fieldNames">更新列集合</param>
        /// <returns></returns>
        public int Update(List<TEntity> list, List<string> fieldNames)
        {
            foreach (var entity in list)
            {

                if (fieldNames != null && fieldNames.Count > 0)
                {
                    DbContext.Set<TEntity>().Attach(entity);
                    foreach (var item in fieldNames)
                    {
                        DbContext.Entry<TEntity>(entity).Property(item).IsModified = true;
                    }
                }
                else
                {
                    DbContext.Entry<TEntity>(entity).State = EntityState.Modified;
                }
            }
            return DbContext.SaveChanges();

        }
        /// <summary>
        /// 异步执行SQL
        /// </summary>
        /// <param name="rawSql"></param>
        /// <param name="pars"></param>
        /// <returns>影响行数</returns>
        public async Task<int> ExecuteCommandAsync(string rawSql, params object[] pars)
        {
            return await this.DbContext.Database.ExecuteSqlRawAsync(rawSql, pars);
        }

        /// <summary>
        /// 执行SQL
        /// </summary>
        /// <param name="rawSql"></param>
        /// <param name="pars"></param>
        /// <returns>影响行数</returns>
        public int ExecuteCommand(string rawSql, params object[] pars)
        {
            return this.DbContext.Database.ExecuteSqlRaw(rawSql, pars);
        }

        /// <summary>
        /// 异步 执行Sql 语句
        /// </summary>
        /// <param name="rawSql">Sql 语句</param>
        /// <param name="pars">参数</param>
        /// <returns>实体集合</returns>
        public async Task<IEnumerable<TEntity>> ExecuteQueryAsync(string rawSql, params object[] pars)
        {
            var queryable = this.DbSet.FromSqlRaw<TEntity>(rawSql, pars);
            return await queryable.ToListAsync();
        }

        /// <summary>
        /// 执行Sql 语句
        /// </summary>
        /// <param name="rawSql">Sql 语句</param>
        /// <param name="pars">参数</param>
        /// <returns>实体集合</returns>
        public IEnumerable<TEntity> ExecuteQuery(string rawSql, params object[] pars)
        {
            var queryable = this.DbSet.FromSqlRaw<TEntity>(rawSql, pars);
            return queryable.ToList();
        }

        /// <summary>
        /// 异步创建
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> CreateAsync(TEntity entity, IRepoOptions options = null)
        {
            await this.DbSet.AddAsync(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
                //如果entity带了关联的子项，数量就不只1了，只要大于0就是通过
                return count > 0 ? entity : default(TEntity);
            }
            else return entity;
        }

        public virtual async Task CreateRangeAsync(IEnumerable<TEntity> entity, IRepoOptions options = null)
        {
            this.DbSet.AddRange(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 创建
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual TEntity Create(TEntity entity, IRepoOptions options = null)
        {
            this.DbSet.Add(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = this.SaveChanges();
                return count == 1 ? entity : default(TEntity);
            }
            else return entity;
        }

        /// <summary>
        /// 异步获取实体
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> sort = null, bool isDesc = false, IRepoOptions options = null)
        {
            IQueryable<TEntity> query = this.DbSet;
            if (condition != null)
            {
                query = query.Where(condition);
            }
            if (sort != null)
            {
                query = isDesc ? query.OrderByDescending(sort) : query.OrderBy(sort);
            }
            return await query.FirstOrDefaultAsync();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual TEntity Get(Expression<Func<TEntity, bool>> condition, IRepoOptions options = null)
        {
            return this.DbSet.FirstOrDefault(condition);
        }
        /// <summary>
        /// 异步 获取列表 带分页
        /// </summary>
        /// <param name="pageable">条件</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<Pageable<TEntity>> ListAsync(Pageable<TEntity> pageable, IRepoOptions options = null)
        {
            var expr = pageable.Expression;
            IQueryable<TEntity> query = this.DbSet;

            if (pageable.Includes != null)
            {
                foreach (var includeExpr in pageable.Includes)
                {
                    query = query.Include(includeExpr);
                }
            }
            if (expr != null)
            {
                query = query.Where(expr);
            }

            long count = 0;
            if (pageable.PageSize != 0)
            {
                if (expr != null)
                {
                    //query = this.DbSet.Where(expr);
                }
                else
                {
                    //query = this.DbSet;
                }
                count = await query.LongCountAsync();
                pageable.RecordCount = count;
                if (count == 0)
                {
                    pageable.Items = new List<TEntity>();
                    return pageable;
                }

            }
            //query = DbSet;
            if (pageable.Asc != null)
            {
                query = query.OrderBy(pageable.Asc);
            }
            else if (pageable.Desc != null)
            {
                query = query.OrderByDescending(pageable.Desc);
            }

            if (pageable.PageSize > 0)
            {
                int pageSize = (int)pageable.PageSize;
                var pageCount = count / pageSize;
                if (count % pageSize > 0) pageCount++;
                pageable.PageCount = pageCount;
                pageable.PageCounts = count;
                var pageIndex = pageable.PageIndex;
                if (pageIndex == 0) pageIndex = pageable.PageIndex = 1;
                if (pageIndex > pageCount) pageIndex = pageable.PageIndex = pageCount;
                int skip = (int)((pageIndex - 1) * pageSize);
                query = query.Skip(skip).Take(pageSize);
            }

            pageable.Items = await query.ToListAsync();
            return pageable;
        }
        /// <summary>
        /// 异步 获取列表 带分页
        /// </summary>
        /// <param name="condition">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListAsync(Expression<Func<TEntity, bool>> condition, Expression<Func<TEntity, object>> sort = null, bool isDesc = false)
        {
            IQueryable<TEntity> query = this.DbSet;
            if (condition != null)
            {
                query = query.Where(condition);
            }
            if (sort != null)
            {
                if (isDesc)
                {
                    query = query.OrderByDescending(sort);
                }
                else
                {
                    query = query.OrderBy(sort);
                }
            }
            return await query.ToListAsync();
        }
        /// <summary>
        /// 查询列表 带分页功能
        /// </summary>
        /// <param name="pageable"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public Pageable<TEntity> List(Pageable<TEntity> pageable, IRepoOptions options = null)
        {
            var expr = pageable.Expression;
            IQueryable<TEntity> query = this.DbSet;

            if (pageable.Includes != null)
            {
                foreach (var includeExpr in pageable.Includes)
                {
                    query = query.Include(includeExpr);
                }
            }
            if (expr != null)
            {
                query = query.Where(expr);
            }

            long count = 0;
            if (pageable.PageSize != 0)
            {
                //if (expr != null)
                //{
                //    //query = this.DbSet.Where(expr);
                //}
                //else
                //{
                //    //query = this.DbSet;
                //}
                count = query.LongCount();
                pageable.RecordCount = count;
                if (count == 0)
                {
                    pageable.Items = new List<TEntity>();
                    return pageable;
                }

            }
            //query = DbSet;
            if (pageable.Asc != null)
            {
                query = query.OrderBy(pageable.Asc);
            }
            else if (pageable.Desc != null)
            {
                query = query.OrderByDescending(pageable.Desc);
            }

            if (pageable.PageSize > 0)
            {
                int pageSize = (int)pageable.PageSize;
                var pageCount = count / pageSize;
                if (count % pageSize > 0) pageCount++;
                pageable.PageCount = pageCount;
                pageable.PageCounts = count;
                var pageIndex = pageable.PageIndex;
                if (pageIndex == 0) pageIndex = pageable.PageIndex = 1;
                if (pageIndex > pageCount) pageIndex = pageable.PageIndex = pageCount;
                int skip = (int)((pageIndex - 1) * pageSize);
                query = query.Skip(skip).Take(pageSize);
            }

            pageable.Items = query.ToList();
            return pageable;
        }



        /// <summary>
        /// 异步获取列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isDesc">排序方式</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> sort = null, bool isDesc = false, IRepoOptions options = null)
        {
            IQueryable<TEntity> queryable = this.DbSet;
            if (where != null) queryable = queryable.Where(where);
            if (sort != null)
            {
                if (isDesc)
                {
                    queryable = queryable.OrderByDescending(sort);
                }
                else
                {
                    queryable = queryable.OrderBy(sort);
                }
            }
            return await queryable.ToListAsync();
        }

        public async Task<IList<TEntity>> ListAsync(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> sort = null, Expression<Func<TEntity, object>> thenBy = null, bool isDesc = false, IRepoOptions options = null)
        {
            IQueryable<TEntity> queryable = this.DbSet;
            if (where != null) queryable = queryable.Where(where);
            if (sort != null)
            {
                if (isDesc)
                {
                    if (thenBy != null)
                    {
                        queryable = queryable.OrderByDescending(sort).ThenBy(thenBy);
                    }
                    else
                    {
                        queryable = queryable.OrderByDescending(sort);
                    }
                }
                else
                {
                    if (thenBy != null)
                    {
                        queryable = queryable.OrderBy(sort).ThenBy(thenBy);
                    }
                    else
                    {
                        queryable = queryable.OrderBy(sort);
                    }
                }
            }
            return await queryable.ToListAsync();
        }
        /// <summary>
        /// 查询列表
        /// </summary>
        /// <param name="where">条件</param>
        /// <param name="sort">排序字段</param>
        /// <param name="isDesc">排序方式</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public IList<TEntity> List(Expression<Func<TEntity, bool>> where, Expression<Func<TEntity, object>> sort = null, bool isDesc = false, IEnumerable<Expression<Func<TEntity, object>>> Includes = null, IRepoOptions options = null)
        {
            IQueryable<TEntity> queryable = this.DbSet;
            if (where != null) queryable = queryable.Where(where);
            if (Includes != null)
            {
                foreach (var includeExpr in Includes)
                {
                    queryable = queryable.Include(includeExpr);
                }
            }
            if (sort != null)
            {
                if (isDesc)
                {
                    queryable.OrderByDescending(sort);
                }
                else
                {
                    queryable.OrderBy(sort);
                }
            }
            return queryable.ToList();
        }
        /// <summary>
        /// 异步修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> ModifyAsync(TEntity entity, IRepoOptions options = null)
        {
            //this.DbSet.Attach(entity);
            var trace = this.DbSet.Update(entity);
            //trace.State = EntityState.Modified;
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
                return count == 1 ? entity : default(TEntity);
            }
            else return entity;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TEntity Modify(TEntity entity, IRepoOptions options = null)
        {
            //this.DbSet.Attach(entity);
            var trace = this.DbSet.Update(entity);
            //trace.State = EntityState.Modified;
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = this.SaveChanges();
                return count == 1 ? entity : default(TEntity);
            }
            else return entity;
        }

        public async Task ModifyRangeAsync(IEnumerable<TEntity> entity, IRepoOptions options = null)
        {
            this.DbSet.UpdateRange(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                await this.SaveChangesAsync();
            }
        }

        /// <summary>
        /// 异步删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteAsync(TEntity entity, IRepoOptions options = null)
        {
            //this.DbSet.Attach(entity);
            var trace = this.DbSet.Remove(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
                return count == 1 ? entity : default(TEntity);
            }
            else return entity;
        }
        /// <summary>
        /// 批量异步删除
        /// </summary>
        /// <param name="entities">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<IList<TEntity>> DeleteAsync(IList<TEntity> entities, IRepoOptions options = null)
        {
            this.DbSet.RemoveRange(entities);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
                return count == entities.Count ? entities : default(IList<TEntity>);
            }
            else return entities;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TEntity Delete(TEntity entity, IRepoOptions options = null)
        {
            //this.DbSet.Attach(entity);
            var trace = this.DbSet.Remove(entity);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = this.SaveChanges();
                return count == 1 ? entity : default(TEntity);
            }
            else return entity;
        }

        /// <summary>
        /// 删除 异步
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public async Task<TEntity> DeleteByIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {
            var entity = await this.GetByIdAsync(id, options);
            if (entity == null) return default(TEntity);
            return await this.DeleteAsync(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键</param>
        /// <param name="options"></param>
        /// <returns></returns>
        public TEntity DeleteById(DbPrimaryType id, IRepoOptions options = null)
        {
            var entity = this.GetById(id, options);
            if (entity == null) return default(TEntity);
            return this.Delete(entity);
        }

        /// <summary>
        /// 异步根据主键获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetByIdAsync(DbPrimaryType id, IRepoOptions options = null)
        {

            var result = await this.DbContext.FindAsync(typeof(TEntity), id);
            return result as TEntity;
        }
        /// <summary>
        /// 根据ID获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public virtual TEntity GetById(DbPrimaryType id, IRepoOptions options = null)
        {

            var result = this.DbContext.Find(typeof(TEntity), id);
            return result as TEntity;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            this.DbContext.Dispose();
        }

        public async Task<Pageable<TValue>> SelectAsync<TValue>(Pageable<TEntity> pageable, Expression<Func<TEntity, TValue>> what, bool distinct = true, IRepoOptions options = null)
        {
            //拷贝enitity的分页信息到selectvalue的分页信息
            Pageable<TValue> selectResult = new Pageable<TValue>()
            {
                PageIndex = pageable.PageIndex,
                PageSize = pageable.PageSize,
                RecordCount = pageable.RecordCount,
                PageCount = pageable.PageCount,
                PageCounts = pageable.PageCounts,
                Items = new List<TValue>()
            };
            if (what == null)
            {
                return selectResult;
            }
            var expr = pageable.Expression;
            IQueryable<TEntity> query = this.DbSet;
            if (pageable.Includes != null)
            {
                foreach (var includeExpr in pageable.Includes)
                {
                    query = query.Include(includeExpr);
                }
            }
            if (expr != null)
            {
                query = query.Where(expr);
            }
            long count = 0;
            if (pageable.PageSize != 0)
            {
                count = await query.LongCountAsync();
                selectResult.RecordCount = count;
                if (count == 0)
                {
                    return selectResult;
                }
            }
            //query = DbSet;
            if (pageable.Asc != null)
            {
                query = query.OrderBy(pageable.Asc);
            }
            else if (pageable.Desc != null)
            {
                query = query.OrderByDescending(pageable.Desc);
            }
            if (selectResult.PageSize > 0)
            {
                int pageSize = (int)selectResult.PageSize;
                var pageCount = count / pageSize;
                if (count % pageSize > 0) pageCount++;
                selectResult.PageCount = pageCount;
                selectResult.PageCounts = count;
                var pageIndex = selectResult.PageIndex;
                if (pageIndex == 0) pageIndex = selectResult.PageIndex = 1;
                if (pageIndex > pageCount) pageIndex = selectResult.PageIndex = pageCount;
                int skip = (int)((pageIndex - 1) * pageSize);
                query = query.Skip(skip).Take(pageSize);
            }
            IQueryable<TValue> selectQuery = query.Select(what);
            if (distinct)
            {
                selectQuery = selectQuery.Distinct();
            }
            selectResult.Items = await selectQuery.ToListAsync();
            return selectResult;
        }

        public async Task<TEntity> SetValueAsync(TEntity src, TEntity dst, IRepoOptions options = null)
        {
            this.DbContext.Entry(src).CurrentValues.SetValues(dst);
            if (options == null || !options.SaveChangeBySelf)
            {
                var count = await this.SaveChangesAsync();
                return count > 0 ? dst : default(TEntity);
            }
            return dst;
        }
    }
}