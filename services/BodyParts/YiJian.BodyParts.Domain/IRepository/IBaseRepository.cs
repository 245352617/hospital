using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Guids;

namespace YiJian.BodyParts.IRepository
{
   public interface IBaseRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        #region 参数

        /// <summary>
        /// 日志
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 构建 Guid
        /// </summary>
        IGuidGenerator GuidGenerator { get; set; }

        #endregion

        IQueryable<TEntity> Set();

        IQueryable<TEntity> WithDetails(IncludeEnum includeDetails);

        /// <summary>
        /// 批量写入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> CreateRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 批量物理删除
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> DeleteRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken = default(CancellationToken));
        
        /// <summary>
        /// 批量更新
        /// </summary>
        /// <param name="list"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> UpdateRangeAsync(List<TEntity> list,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 根据linq表达式，返回一个匿名列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="selector"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <returns></returns>
        Task<List<dynamic>> GeListDynamictAsyn(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, dynamic>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken));


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
        Task<List<Dto>> Dtos<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1) where Dto : class, new();

        /// <summary>
        /// 根据linq表达式，返回一个匿名或Dto列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="mapperFunc"></param>
        /// <param name="includeDetails"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        Task<List<Dto>> GetListDtoAsyn<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
            where Dto : class, new();

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
        Task<List<Dto>> ToDtoList<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1)
            where Dto : class, new();


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
        Task<Dto> FirstOrDefaultDtoAsync<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new();

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
        Task<Dto> FirstOrDefaultDtoAsync<Dto>(TKey Id,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new();

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
        Task<Dto> DtoAsync<Dto>(Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, Dto>> selector = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = 1)
            where Dto : class, new();


        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        Task<List<TEntity>> DataList(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1);

        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <param name="sorting"></param>
        /// <param name="cancellationToken"></param>
        /// <param name="Top"></param>
        /// <returns></returns>
        Task<List<TEntity>> ToList(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1);

        /// <summary>
        /// 根据linq表达式，返回一个列表
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails">是否取出导航属性信息</param>
        /// <returns></returns>
        Task<List<TEntity>> GetListLinqAsyn(Expression<Func<TEntity, bool>> predicate,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            string sorting = null,
            CancellationToken cancellationToken = default(CancellationToken),
            int Top = -1);

        /// <summary>
        /// 获取一个模型对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> FirstOrDefaultLinqAsyn(
            Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// 获取一个模型对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sorting"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetModelAsync(Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 获取一个模型对象
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="sorting"></param>
        /// <param name="includeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<TEntity> GetModelAsyn(Expression<Func<TEntity, bool>> predicate,
            string sorting = null,
            IncludeEnum includeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken));

        #region 提交保存

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        #endregion

        #region 提交保存

        /// <summary>
        /// 通用查询
        /// </summary>
        /// <param name="filter">过滤对象</param>
        /// <returns></returns>
        Task<dynamic> Where(
            EfDbFilter<TEntity, TKey> filter,
            CancellationToken cancellationToken
        );

        Task<dynamic> Where<TDto>(
            EfDbFilter<TEntity, TKey, TDto> filter,
            CancellationToken cancellationToken
        );

        #endregion

        #region 通用自定义分页

        /// <summary>
        /// 通用自定义分页
        /// </summary>
        /// <param name="filter">分页或过滤对象</param>
        /// <returns></returns>
        Task<PageReturnResult> PageAsync(
            EfDbFilter<TEntity, TKey> filter,
            CancellationToken cancellationToken
        );

        Task<PageReturnResult> PageAsync<TDto>(
            EfDbFilter<TEntity, TKey, TDto> filter,
            CancellationToken cancellationToken
        );

        #endregion

        #region vue element-ui table,分页

        /// <summary>
        /// vue element-ui table,分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="title"></param>
        /// <param name="Where"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        // Task<ReturnResult> PageVue<TDto>(
        //     PagedAndSortedMultipleWhereResultRequest page,
        //     string title,
        //     Expression<Func<TEntity, bool>> Where,
        //     Expression<Func<TEntity, TDto>> Selector = null,
        //     IncludeEnum IncludeDetails = IncludeEnum.不包含导航属性,
        //     CancellationToken cancellationToken = default(CancellationToken));

        #endregion

        /// <summary>
        /// 计算总数
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> predicate,
            CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// 简单分页
        /// </summary>
        /// <param name="page"></param>
        /// <param name="Where"></param>
        /// <param name="IncludeDetails"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        Task<PageReturnResult<List<TDto>>> PageSimple<TDto>(
            PagedAndSortedMultipleWhereResultRequest page,
            Expression<Func<TEntity, bool>> Where,
            IncludeEnum IncludeDetails = IncludeEnum.不包含导航属性,
            CancellationToken cancellationToken = default(CancellationToken),
            Expression<Func<TEntity, TDto>> SelectorDto = null);

        /// <summary>
        /// 返回泛型的分页列表
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="cancellationToken"></param>
        /// <typeparam name="Dto"></typeparam>
        /// <returns></returns>
        Task<PageReturnResult<List<Dto>>> PageTAsync<Dto>(
            EfDbFilter<TEntity, TKey, Dto> filter,
            CancellationToken cancellationToken
        );

        /// <summary>
        /// 从Dto中保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <param name="KeyName">主键</param>
        /// <param name="notUpdateField">不更新的字体</param>
        /// <typeparam name="TDto"></typeparam>
        /// <returns></returns>
        Task<ReturnResult<bool>> UpdateFromDto<TDto>(TEntity data, TDto model, string KeyName = "Id",
            List<string> notUpdateField = null,CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 自定义更新方法
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> UpdateAbpAsync(TEntity entity,
            CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// 新增 or 更新
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<ReturnResult<bool>> AddOrUpdate<TDto>(TDto model, string primoryKey = "Id",
            CancellationToken cantoken = default(CancellationToken)) where TDto : class;

        #region 数据是否存在

        /// <summary>
        /// 数据是否存在
        /// </summary>
        /// <param name="where"></param>
        /// <param name="isGetFirst">如何有值取一个还是多个数据</param>
        /// <returns></returns>
        Task<ReturnResult<bool>> IsExist(Expression<Func<TEntity, bool>> where,
            Expression<Func<TEntity, dynamic>> selectDto = null, bool isGetFirst = true);

        #endregion

        /// <summary>
        /// 保存数据库修改
        /// </summary>
        /// <returns></returns>
        Task SaveChangesAsync();
    }
}
