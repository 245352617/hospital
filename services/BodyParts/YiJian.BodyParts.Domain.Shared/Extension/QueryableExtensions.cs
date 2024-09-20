using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YiJian.BodyParts.Domain.Shared.Extension
{

    /// <summary>
    /// Queryable扩展方法
    /// </summary>
    public static class QueryableExtensions
    {

        /// <summary>
        /// 数据分页扩展
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isOnlyTotal">是否只读统计</param>
        /// <returns></returns>
        public static PageList<T> PageList<T>(this IQueryable<T> source, int? pageIndex, int? pageSize, bool isOnlyTotal = false) where T : class, new()
        {
            try
            {
                if (source == null)
                {
                    return null;
                }
                //实例类
                PageList<T> pageList = new PageList<T>();

                //当前页无值或者小于0
                if (pageIndex <= 0 || !pageIndex.HasValue)
                {
                    pageIndex = 0;
                }

                //起始页赋值
                pageList.PageIndex = pageIndex.Value;

                //页大小有值时
                if (pageSize.HasValue)
                    pageList.PageSize = pageSize.Value;
                else
                    pageList.PageSize = source.Count();

                //总条数
                pageList.TotalCount = source.Count();

                //页总数，size为0可能会报错
                pageList.TotalPages = pageList.TotalCount / (pageList.PageSize == 0 ? 1 : pageList.PageSize);

                //是否只读
                if (isOnlyTotal)
                {
                    return pageList;
                }

                pageList.Data = source.Skip((pageList.PageIndex - 1) * pageList.PageSize).Take(pageList.PageSize).ToList();

                return pageList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 数据分页扩展
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageIndex">当前页</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="isOnlyTotal">是否只读统计</param>
        /// <returns></returns>
        public static async Task<PageList<T>> PageListAsync<T>(this IQueryable<T> source, int? pageIndex, int? pageSize, bool isOnlyTotal = false) where T : class, new()
        {
            try
            {
                return await Task.Run(() =>
                {
                    if (source == null)
                    {
                        return null;
                    }
                    else
                    {
                        return source.PageList<T>(pageIndex, pageSize, isOnlyTotal);
                    }
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }



    /// <summary>
    /// 分页实体
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageList<T> where T :class,new()
    {
        /// <summary>
        /// 当前页
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 总行数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public List<T> Data { get; set; }

    }
}
