using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace YiJian.BodyParts
{
    /// <summary>
    /// Linq 多条件拼接查询 扩展类
    /// </summary>
    public static class PredicateBuilder
    {
        public static Expression<Func<T, bool>> True<T>()
        { return f => true; }
        public static Expression<Func<T, bool>> False<T>() { return f => false; }

        public static Expression<Func<T, bool>> Or<T>(this Expression<Func<T, bool>> expr1,
                                                            Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.Or(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> And<T>(this Expression<Func<T, bool>> expr1,
                                                             Expression<Func<T, bool>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters.Cast<Expression>());
            return Expression.Lambda<Func<T, bool>>
                  (Expression.And(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Expression<Func<T, bool>> AndIf<T>(this Expression<Func<T, bool>> expr1, bool condition,
            Expression<Func<T, bool>> expr2)
        {
            return !condition ? expr1 : expr1.And(expr2);
        }

        /// <summary>
        /// 获取Lambda中的列名
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKeys"></typeparam>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<string> GetKeys<T, TKeys>(this Expression<Func<T, TKeys>> selector) {
            List<string> cols = new List<string>();
            if (selector.Body is NewExpression) {
                var temp = (NewExpression)selector.Body;

                foreach (var val in temp.Members) {
                    cols.Add(val.Name);
                }
            } else {
                var temp = (MemberExpression)selector.Body;
                cols.Add(temp.Member.Name);
            }

            return cols;
        }
    }
}
