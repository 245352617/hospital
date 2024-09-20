using YiJian.BodyParts.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace YiJian.BodyParts.Repository
{
    public class BaseRepository<TDbContext, TEntity, TKey> : EfCoreRepository<TDbContext, TEntity, TKey>,
        IBaseRepository<TEntity, TKey>
        where TDbContext : IEfCoreDbContext
        where TEntity : class, IEntity<TKey>
    {

        #region 参数

        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger
        {
            get { return this._lazyLogger.Value; }
        }

        /// <summary>
        /// 构建 Guid
        /// </summary>
        public IGuidGenerator GuidGenerator { get; set; }

        #endregion

        #region LoggerFactory

        private ILoggerFactory _loggerFactory;
        private readonly object _baseRepositoryProviderLock = new object();

        protected TService LazyGetRequiredService<TService>(ref TService reference)
        {
            return this.LazyGetRequiredService<TService>(typeof(TService), ref reference);
        }

        public ILoggerFactory LoggerFactory
        {
            get { return this.LazyGetRequiredService<ILoggerFactory>(ref this._loggerFactory); }
        }

        protected TRef LazyGetRequiredService<TRef>(Type serviceType, ref TRef reference)
        {
            if ((object)reference == null)
            {
                lock (this._baseRepositoryProviderLock)
                {
                    if ((object)reference == null)
                        reference = (TRef)ServiceProviderServiceExtensions.GetRequiredService(this.ServiceProvider,
                            serviceType);
                }
            }

            return reference;
        }

        private Lazy<ILogger> _lazyLogger
        {
            get
            {
                return new Lazy<ILogger>(
                    (Func<ILogger>)(() =>
                       this.LoggerFactory?.CreateLogger(this.GetType().FullName) ?? (ILogger)NullLogger.Instance),
                    true);
            }
        }

        #endregion

        #region 构造方法

        public BaseRepository(IDbContextProvider<TDbContext> dbContextProvider
        ) : base(dbContextProvider)
        {
        }

        public IQueryable<TEntity> Set()
        {
            return GetQueryable().AsNoTracking();
        }

        public virtual IQueryable<TEntity> WithDetails(IncludeEnum includeDetails)
        {
            switch (includeDetails)
            {
                case IncludeEnum.不包含导航属性:
                    return Set();
            }

            return WithDetails();
        }

        #endregion

        /// <summary>
        ///  批量写入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public virtual async Task<ReturnResult<bool>> CreateRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken)
        {
            if (list.IsEmptyOrNull()) return ReturnResult<bool>.RequestParamsIsNull();

            await DbContext.Set<TEntity>().AddRangeAsync(list, cancellationToken);

            var r = await DbContext.SaveChangesAsync(cancellationToken);

            return ReturnResult<bool>.Ok($"操作完成，共写入{r}条数据", true);
        }

        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> DeleteRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken = default(CancellationToken)) //where TKey2:Guid
        {
            if (list.IsEmptyOrNull()) return ReturnResult<bool>.RequestParamsIsNull();

            foreach (var row in list)
            {
                DbContext.Entry(row).State = EntityState.Deleted;
            }

            var r = await DbContext.SaveChangesAsync(cancellationToken);

            return ReturnResult<bool>.Ok($"操作完成，传入{list.Count()}条数据，共删除{r}条数据", true);
        }

        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> UpdateRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (list.IsEmptyOrNull()) return ReturnResult<bool>.RequestParamsIsNull();

            foreach (var row in list)
            {
                DbContext.Entry(row).State = EntityState.Modified;
            }

            var r = await DbContext.SaveChangesAsync(cancellationToken);

            return ReturnResult<bool>.Ok($"操作完成，传入{list.Count()}条数据，共更新{r}条数据", true);
        }

        /// <summary>
        /// 根据linq表达式，返回一个匿名列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <returns></returns>
        public virtual async Task<List<dynamic>> GeListDynamictAsyn(
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, dynamic>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            if (selector == null)
            {
                selector = CreateSelecterExpressionExten.CreateSelecter<TEntity, dynamic>();
            }

            if (sorting.IsEmptyOrNull())
            {
                return await WithDetails(includeDetails).Where(predicate).Select(selector)
                    .ToListAsync(cancellationToken);
            }

            return await ApplySorting(WithDetails(includeDetails).Where(predicate), sorting).Select(selector)
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 根据linq表达式，返回一个匿名或Dto列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<List<Dto>> Dtos<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
            where Dto : class, new()
        {
            return await GetListDtoAsyn<Dto>(predicate: predicate,
                selector: selector,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken,
                Top: Top
            );
        }

        /// <summary>
        /// 根据linq表达式，返回一个匿名或Dto列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<List<Dto>> ToDtoList<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
            where Dto : class, new()
        {
            return await GetListDtoAsyn<Dto>(predicate: predicate,
                selector: selector,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken,
                Top: Top
            );
        }

        /// <summary>
        /// 获取一个Dto
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<Dto> FirstOrDefaultDtoAsync<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new()
        {
            return (await GetListDtoAsyn<Dto>(predicate: predicate,
                selector: selector,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken,
                Top: Top
            ))?.FirstOrDefault();
        }

        /// <summary>
        /// 获取一个Dto
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<Dto> FirstOrDefaultDtoAsync<Dto>(TKey Id,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new()
        {
            Expression<Func<TEntity, bool>> predicate = a => true;

            if (Id is Guid id)
            {
                predicate = predicate.And(a => EF.Property<Guid>(a, "Id") == id);
            }
            else if (Id is Int32 intId)
            {
                predicate = predicate.And(a => EF.Property<int>(a, "Id") == intId);
            }
            else
            {
                throw new Exception("FirstOrDefaultDtoAsync<Dto>.TKey 类型不是Guid和Int32，请重写当前方法");
            }

            return await FirstOrDefaultDtoAsync(predicate, selector, includeDetails, sorting, cancellationToken,
                Top);
        }

        /// <summary>
        /// 获取一个Dto
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<Dto> DtoAsync<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new()
        {
            return (await GetListDtoAsyn<Dto>(predicate: predicate,
                selector: selector,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken,
                Top: Top
            ))?.FirstOrDefault();
        }

        /// <summary>
        /// 根据linq表达式，返回一个匿名或Dto列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<List<Dto>> GetListDtoAsyn<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
            where Dto : class, new()
        {
            selector ??= CreateSelecterExpressionExten.CreateSelecter<TEntity, Dto>();

            IQueryable<Dto> r = sorting.IsEmptyOrNull()
                ? WithDetails(includeDetails).Where(predicate).Select(selector)
                : ApplySorting(WithDetails(includeDetails).Where(predicate), sorting).Select(selector);

            if (Top > 0) r = r.Take(Top);

            return await r.ToListAsync(cancellationToken);
        }

        #region 根据linq表达式，返回一个列表

        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> DataList(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
        {
            return await GetListLinqAsyn(
                predicate: predicate,
                includeDetails: includeDetails,
                sorting: sorting,
                cancellationToken: cancellationToken,
                Top: Top
            );
        }

        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> ToList(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
        {
            return await GetListLinqAsyn(
                predicate: predicate,
                includeDetails: includeDetails,
                sorting: sorting,
                cancellationToken: cancellationToken,
                Top: Top
            );
        }

        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <returns></returns>
        public virtual async Task<List<TEntity>> GetListLinqAsyn(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
        {
            IQueryable<TEntity> r = sorting.IsEmptyOrNull()
                ? WithDetails(includeDetails).Where(predicate)
                : ApplySorting(WithDetails(includeDetails).Where(predicate), sorting);

            if (Top > 0) r = r.Take(Top);

            return await r.ToListAsync(cancellationToken);
        }

        #endregion

        #region

        /// <summary>
        /// 获取一个对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> FirstOrDefaultLinqAsyn(Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetModelAsyn(predicate: predicate,
                sorting: sorting,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken
            );
        }

        /// <summary>
        /// 获取一个模型对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sorting"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetModelAsyn(Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var list = WithDetails(includeDetails).Where(predicate);

            if (sorting.IsNotEmptyOrNull() && list.IsNotEmptyOrNull())
            {
                list = ApplySorting(list, sorting);
            }

            return (await list?.ToListAsync()).FirstOrDefault();
        }

        /// <summary>
        /// 获取一个模型对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sorting"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public virtual async Task<TEntity> GetModelAsync(Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await GetModelAsyn(predicate: predicate,
                sorting: sorting,
                includeDetails: includeDetails,
                cancellationToken: cancellationToken
            );
        }

        #endregion

        #region 提交保存

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var r = await DbContext.SaveChangesAsync(cancellationToken);

            return r;
        }

        #endregion

        #region 条件查询

        public virtual async Task<dynamic> Where<Dto>(
            EfDbFilter<TEntity, TKey, Dto> filter,
            CancellationToken cancellationToken
        )
        {
            var list = WithDetails(filter.IncludeDetails).AsNoTracking();

            list = list.Where(filter.Where);

            if (filter.GetCount != null)
            {
                filter.GetCount(await list.CountAsync(cancellationToken));
            }

            if (filter.OrderByField.IsNotEmptyOrNull())
            {
                list = ApplySorting(list, filter.OrderByField);
            }
            else if (filter.OrderBy != null)
            {
                switch (filter.OrderDirect)
                {
                    case OrderDirectEnum.升序:
                        list = list.OrderBy(filter.OrderBy);
                        break;
                    case OrderDirectEnum.降序:
                        list = list.OrderByDescending(filter.OrderBy);
                        break;
                }
            }

            if (filter.PageIndex > -1 && filter.PageSize > 0)
            {
                list = list.Skip((filter.PageIndex - 1) * filter.PageSize).Take(filter.PageSize);
            }

            if (filter.SelectorDto != null)
            {
                return await list.Select(filter.SelectorDto).ToListAsync(cancellationToken);
            }

            if (filter.Selector != null)
            {
                return await list.Select(filter.Selector).ToListAsync(cancellationToken);
            }

            return await list.ToListAsync(cancellationToken);
        }

        /// <summary>
        /// 通用查询,条件查询
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public virtual async Task<dynamic> Where(
            EfDbFilter<TEntity, TKey> filter,
            CancellationToken cancellationToken
        )
        {
            return await Where<TEntity>((EfDbFilter<TEntity, TKey, TEntity>)filter, cancellationToken);
        }

        #endregion

        #region 通用自定义分页

        public async Task<PageReturnResult> PageAsync<Dto>(
            EfDbFilter<TEntity, TKey, Dto> filter,
            CancellationToken cancellationToken
        )
        {
            PageReturnResult result = new PageReturnResult();

            if (filter == null) filter = new EfDbFilter<TEntity, TKey, Dto> { PageIndex = 1, PageSize = 15 };

            if (filter.GetCount == null)
            {
                filter.GetCount = (a) =>
                {
                    result.TotalCount = a;
                    return null;
                };
            }

            result.PageSize = filter.PageSize;

            result.PageIndex = filter.PageIndex;

            result.Items = await Where(filter, cancellationToken: cancellationToken);

            return result;
        }

        /// <summary>
        /// 返回泛型的分页列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        public async Task<PageReturnResult<List<Dto>>> PageTAsync<Dto>(
            EfDbFilter<TEntity, TKey, Dto> filter,
            CancellationToken cancellationToken
        )
        {
            PageReturnResult<List<Dto>> result = new PageReturnResult<List<Dto>>();

            if (filter == null) filter = new EfDbFilter<TEntity, TKey, Dto> { PageIndex = 1, PageSize = 15 };

            if (filter.GetCount == null)
            {
                filter.GetCount = (a) =>
                {
                    result.TotalCount = a;
                    return null;
                };
            }

            result.PageSize = filter.PageSize;

            result.PageIndex = filter.PageIndex;

            result.Items = (List<Dto>)(await Where(filter, cancellationToken: cancellationToken));

            return result;
        }

        /// <summary>
        /// 通用自定义分页
        /// </summary>
        /// <param name="filter">分页、过滤对象</param>
        /// <returns></returns>
        public async Task<PageReturnResult> PageAsync(
            EfDbFilter<TEntity, TKey> filter,
            CancellationToken cancellationToken
        )
        {
            return await PageAsync<TEntity>((EfDbFilter<TEntity, TKey, TEntity>)filter, cancellationToken);
        }

        #endregion

        IQueryable<TEntity> ApplySorting(
            IQueryable<TEntity> query,
            string input)
        {
            return (IQueryable<TEntity>)DynamicQueryableExtensions.OrderBy<TEntity>(query, input,
                Array.Empty<object>());
        }

        /// <summary>
        /// 简单分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Where"></param>
        /// <param name="IncludeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public virtual async Task<PageReturnResult<List<TDto>>> PageSimple<TDto>(
            PagedAndSortedMultipleWhereResultRequest page,
            Expression<Func<TEntity, bool>> Where,
            IncludeEnum IncludeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken),
            Expression<Func<TEntity, TDto>> SelectorDto = null)
        {
            if (SelectorDto == null)
                SelectorDto = CreateSelecterExpressionExten.CreateSelecter<TEntity, TDto>();

            var list = await PageTAsync(
                filter: new EfDbFilter<TEntity, TKey, TDto>
                {
                    Where = Where,
                    IncludeDetails = IncludeDetails,
                    PageIndex = page.SkipCount,
                    SelectorDto = SelectorDto,
                    PageSize = page.MaxResultCount,
                    OrderByField = page.Sorting
                }, cancellationToken);

            return list;
        }

        #region vue element-ui table,分页

        // /// <summary>
        // /// vue element-ui table,分页
        // /// </summary>
        // /// <param name="page"></param>
        // /// <param name="title"></param>
        // /// <param name="Where"></param>
        // /// <param name="cancellationToken"></param>
        // /// <typeparam name="TDto"></typeparam>
        // /// <returns></returns>
        // public virtual async Task<ReturnResult> PageVue<TDto>(
        //     PagedAndSortedMultipleWhereResultRequest page,
        //     string title,
        //     Expression<Func<TEntity, bool>> Where,
        //     Expression<Func<TEntity, TDto>> Selector = null,
        //     IncludeEnum IncludeDetails = IncludeEnum.不包含导航属性,
        //     CancellationToken cancellationToken = default(CancellationToken))
        // {
        //    
        // }

        #endregion

        #region 计算总数

        /// <summary>
        /// 计算总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            var c = await this.DbSet.CountAsync(predicate, cancellationToken);

            return c;
        }

        #endregion

        #region 从Dto中保存数据

        /// <summary>
        /// 从Dto中保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="KeyName">主键</param>
        /// <param name="notUpdateField">不更新的字体</param>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> UpdateFromDto<TDto>(TEntity data, TDto model, string KeyName = "Id",
            List<string> notUpdateField = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            var atype = data.GetType();

            var allpros = atype.GetProperties();

            var allpros2 = model.GetType().GetProperties();

            if (notUpdateField.IsEmptyOrNull())
                notUpdateField = new List<string> { "CreationTime", "LastModificationTime" };

            foreach (var row in allpros2)
            {
                if (row.Name == KeyName ||
                    (notUpdateField.IsNotEmptyOrNull() && notUpdateField.Contains(row.Name))) continue;

                var idval = row.GetValue(model, null);
                if (idval != null)
                {
                    var idpro = allpros.FirstOrDefault(a => a.Name == row.Name);
                    if (idpro != null && idpro.CanWrite)
                    {
                        idpro.SetValue(data, idval);
                        this.DbContext.Entry(data).Property(idpro.Name).IsModified = true;
                    }
                }
            }

            //this.DbContext.Entry(data).State = EntityState.Modified;

            var r = await this.DbContext.SaveChangesAsync(cancellationToken);

            return ReturnResult<bool>.Ok(r > 0 ? "更新成功" : "更新失败", data: true);
        }

        #endregion

        #region 新增 or 更新

        /// <summary>
        /// from 新增 or 更新 数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> AddOrUpdate<TDto>(TDto model, [CanBeNull] string primoryKey = "Id",
            CancellationToken cantoken = default(CancellationToken)) where TDto : class
        {
            //return ReturnResult<bool>.Fail("正在开发中");

            var allprops = typeof(TDto).GetProperties();

            if (allprops?.Any(a => a.Name == primoryKey) == false)
            {
                return false.ToReturnResult().SetError().SetMsg($"当前对象中不存在主键,{primoryKey}");
            }

            var idprp = allprops?.FirstOrDefault(a => a.Name == primoryKey);

            var idval = idprp == null ? null : idprp.GetValue(model, null);

            if ((idval is int && (int)idval == 0) ||
                (idval is string s && (s.IsNullOrEmpty()))
                || idval == null ||
                ((idval is Guid) && (Guid)idval == Guid.Empty) ||
                ((idval is Guid?) && (Guid?)idval == Guid.Empty))
            {
                var nobj = await InsertAsync(JsonHelper.DeserializeObjectToObject<TEntity>(model),
                    cancellationToken: cantoken);

                return true.ToReturnResult().SetMsg("新增成功").SetExten(nobj.Id);
            }

            TEntity data = null;

            if (idval is int idvalInt)
            {
                data = await FirstOrDefaultLinqAsyn(a => EF.Property<int>(a, primoryKey) == idvalInt,
                    cancellationToken: cantoken);
            }
            else if (idval is string)
            {
                var idvalstring = (string)idval;
                data = await FirstOrDefaultLinqAsyn(a => EF.Property<string>(a, primoryKey) == idvalstring,
                    cancellationToken: cantoken);
            }
            else if (idval is Guid || idval is Guid?)
            {
                var idvalgid = (Guid)idval;
                data = await FirstOrDefaultLinqAsyn(a => EF.Property<Guid>(a, primoryKey) == idvalgid,
                    cancellationToken: cantoken);
            }
            else
            {
                return false.ToReturnResult().SetError()
                    .SetMsg($"此方法不支持些类型({idprp.PropertyType.Name})进行更新,{primoryKey}:{idval}");
            }

            if (data == null)
            {
                return false.ToReturnResult().SetError().SetMsg($"未找到相关数据，无法更新,{primoryKey}:{idval}");
            }

            var b = await UpdateFromDto(data, model);

            return b;
        }

        /// <summary>
        /// 自定义更新方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> UpdateAbpAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            DbContext.Set<TEntity>().Attach(entity);

            PropertyInfo[] props = entity.GetType().GetProperties();

            List<string> flist = new List<string> { "Id", "CreationTime", "LastModificationTime" };

            var isSave = false;

            foreach (PropertyInfo prop in props)
            {
                var t = prop.PropertyType;
                if (flist.Contains(prop.Name) || (!prop.CanWrite) || !prop.CanRead
                    || t.IsGenericType || t.IsClass || t.IsArray) continue;

                //ConsoleExten.WriteLine($"prop.{prop.Name}:{prop.DeclaringType.Name},{prop.PropertyType.Name}");

                if (prop.GetValue(entity, null) != null)
                {
                    if (prop.GetValue(entity, null)?.ToString() == " ")
                        DbContext.Entry(entity).Property(prop.Name).CurrentValue = null;

                    DbContext.Entry(entity).Property(prop.Name).IsModified = true;
                    isSave = true;
                }
            }

            if (!isSave)
            {
                return false.ToReturnResult().SetMsg("无字段要更新");
            }

            DbContext.Entry(entity).State = EntityState.Modified;

            return ((await DbContext.SaveChangesAsync(cancellationToken)) > 0).ToReturnResult();
        }

        #endregion

        #region 数据是否存在

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <param name="selectDto">查询对象</param>
        /// <param name="isGetFirst">如何有值取一个还是多个数据</param>
        /// <returns></returns>
        public async Task<ReturnResult<bool>> IsExist(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, dynamic>> selectDto = null, bool isGetFirst = true)
        {
            var b = (await this.WithDetails().AsNoTracking().CountAsync(where)) > 0;

            var result = b.ToReturnResult();

            if (b && selectDto != null)
            {
                var list = this.WithDetails().AsNoTracking().Where(where).Select(selectDto);
                if (isGetFirst)
                    result.Exten = await list.FirstOrDefaultAsync();
                else result.Exten = await list.ToListAsync();
            }

            return result;
        }

        public async Task SaveChangesAsync()
        {
            await DbContext.SaveChangesAsync();
        }

        #endregion

    }
}
