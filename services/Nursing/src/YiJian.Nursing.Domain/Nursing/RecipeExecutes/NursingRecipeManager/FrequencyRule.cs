namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 频次规则
    /// </summary>
    internal sealed class FrequencyRule
    {
        /// <summary>
        /// 药品频次信息
        /// </summary>
        public Frequency Frequency { get; set; }

        /// <summary>
        /// 频次单位类型
        /// </summary>
        public FrequencyUnitType UnitType { get; set; }

        /// <summary>
        /// 周期位移
        /// </summary>
        public int UnitOffset { get; set; }
    }
}
