namespace YiJian.ECIS.ShareModel.Extensions;

/// <summary>
/// 集合扩展
/// </summary>
public static class EnumerableExtensions
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="Tkey"></typeparam>
    /// <param name="source"></param>
    /// <param name="keySelector"></param>
    /// <returns></returns>
    public static IEnumerable<TSource> CustomDistinctBy<TSource, Tkey>(this IEnumerable<TSource> source, Func<TSource, Tkey> keySelector)
    {
        HashSet<Tkey> hashSet = new HashSet<Tkey>();
        foreach (TSource item in source)
        {
            if (hashSet.Add(keySelector(item)))
            {
                yield return item;
            }
        }
    }
}