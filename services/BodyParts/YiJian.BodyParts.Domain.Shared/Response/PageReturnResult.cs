namespace YiJian.BodyParts
{
    /// <summary>
    /// 建议使用泛型返回分页类型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class PageReturnResult<T>
    {
        public int TotalCount { get; set; }
        
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }


        private int _PageCount = -1;
        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (_PageCount > -1) return _PageCount;
                
                if (PageSize > 0 && TotalCount > 0)
                {
                    var p = (int) (TotalCount / PageSize);
                    if (TotalCount % PageSize != 0) p += 1;
                    return p;
                }

                return 0;
            }
            set => _PageCount = value;
        }
        
        public T Items { get; set; }
    }
    
    /// <summary>
    /// 分页返回对象
    /// </summary>
    public class PageReturnResult
    {
        public int TotalCount { get; set; }
        
        public int PageIndex { get; set; }
        
        public int PageSize { get; set; }

        /// <summary>
        /// 总页数
        /// </summary>
        public int PageCount
        {
            get
            {
                if (PageSize > 0 && TotalCount > 0)
                {
                    var p = (int) (TotalCount / PageSize);
                    if (TotalCount % PageSize != 0) p += 1;
                    return p;
                }

                return 0;
            }
        }
        
        public dynamic Items { get; set; }
    }
}