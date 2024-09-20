namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描    述 ：护士站医嘱类型配置
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/25 14:46:27
    /// </summary>
    public class NursingRecipeTypeDto
    {
        /// <summary>
        /// 类别名称
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 途径编码
        /// </summary>
        public string UsageCode { get; set; }

        /// <summary>
        /// 途径名称
        /// </summary>
        public string UsageName { get; set; }

    }
}
