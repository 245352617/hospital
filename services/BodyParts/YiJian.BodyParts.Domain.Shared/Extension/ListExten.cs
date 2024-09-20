using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YiJian.BodyParts
{
    public static class ListExten
    {
        /// <summary>
        /// 字符串为空?
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsEmptyOrNull<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() <= 0;
        }

        /// <summary>
        /// 是否不为空
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNotEmptyOrNull<T>(this IEnumerable<T> list)
        {
            return list != null && list.Count() > 0;
        }
    }
}
