using System;
using System.Linq.Expressions;

namespace YiJian.ECIS.Core
{
    /// <summary>
    /// 分页
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Pageable<T> : ListParameters<T>, IPageInfo
    {
        public Pageable(Expression<Func<T, bool>> initCriteria = null) : base(initCriteria)
        {
        }

        public Pageable(IPageInfo pageInfo) : base()
        {
            if (pageInfo != null)
            {
                this.PageIndex = pageInfo.PageIndex;
                this.PageSize = pageInfo.PageSize;
            }
        }

        /// <summary>
        /// 总页数
        /// </summary>
        public long PageCount { get; set; }
        /// <summary>
        /// 总条数
        /// </summary>
        public long PageCounts { get; set; }
        /// <summary>
        /// 当前页
        /// </summary>

        public long PageIndex { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>

        public uint PageSize { get; set; }

        /// <summary>
        /// 转换方式
        /// </summary>
        /// <param name="expr"></param>
        static public implicit operator Pageable<T>(Expression<Func<T, bool>> expr)
        {
            return new Pageable<T>(expr);
        }


    }
}
