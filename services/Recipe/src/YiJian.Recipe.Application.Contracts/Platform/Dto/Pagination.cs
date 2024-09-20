using System.Collections.Generic;

namespace YiJian.Platform.Dto
{
    public class Pagination<T>
    {
        public int TotalCount { get; set; }

        public IEnumerable<T> Items { get; set; }
    }
}
