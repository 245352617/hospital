using System;
using System.Linq.Expressions;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts
{
    public class EfDbFilter<TEntity, TKey,TDto>:EfDbFilter<TEntity,TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 查询字段，影射Dto
        /// </summary>
        public virtual Expression<Func<TEntity, TDto>> SelectorDto { get; set; }
    }
    
    /// <summary>
    /// 分布，排序对象
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public class EfDbFilter<TEntity,TKey> where TEntity : IEntity<TKey>
    {
        /// <summary>
        /// 查询条件 predicate
        /// </summary>
        public virtual Expression<Func<TEntity, bool>> Where { get; set; } = a => true;
        
        /// <summary>
        /// 查询字段，影射Dto
        /// </summary>
        public virtual Expression<Func<TEntity, dynamic>> Selector { get; set; }
        
        public int PageIndex { get; set; } = 1;

        public int PageSize { get; set; } = 15;
        
        public Func<int,dynamic> GetCount { get; set; }

        /// <summary>
        /// 主键排序
        /// </summary>
        public Expression<Func<TEntity, TKey>> OrderBy { get; set; } = null;

        /// <summary>
        /// 排序字段，如果OrderBy为空，刚按这个字段排序 如:Id desc,CreationTime desc
        /// </summary>
        public string OrderByField { get; set; }

        /// <summary>
        /// 降序或升序
        /// </summary>
        public OrderDirectEnum OrderDirect { get; set; } = OrderDirectEnum.降序;
        
        /// <summary>
        /// 包含导航属性
        /// </summary>
        public IncludeEnum IncludeDetails { get; set; } = IncludeEnum.不包含导航属性;
    }
}