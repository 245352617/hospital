namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 电子病历汇总模型
    /// </summary>
    public class EmrCategoryCountDto
    {
        /// <summary>
        /// 属性值
        /// </summary> 
        public string Value { get; set; }

        /// <summary>
        /// 属性标签
        /// </summary> 
        public string Label { get; set; }

        /// <summary>
        /// 汇总总数
        /// </summary>
        public int Count { get; set; } = 0;
    }

}
