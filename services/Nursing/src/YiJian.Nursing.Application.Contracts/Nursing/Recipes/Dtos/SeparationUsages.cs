namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：分方途径扁平化处理
    /// 创建人： yangkai
    /// 创建时间：2023/3/25 9:44:27
    /// </summary>
    public class SeparationUsages
    {
        /// <summary>
        /// 分方名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分方编码
        /// </summary>
        public int Code { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        public string UsageName { get; set; }
    }
}
