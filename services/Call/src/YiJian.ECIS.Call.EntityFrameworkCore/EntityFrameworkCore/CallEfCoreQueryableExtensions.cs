namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using YiJian.ECIS.Call.Domain;

    public static class CallEfCoreQueryableExtensions
    {
        public static IQueryable<Department> IncludeDetails(this IQueryable<Department> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.ConsultingRooms);
        }

    }
}
