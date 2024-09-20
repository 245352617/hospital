using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 医师详细报表请求参数
    /// </summary>
    public class VeidooDetailRequestDto
    {
        /// <summary>
        /// 维度 0=月度,1=季度,2=年度
        /// </summary>
        public EVeidoo Veidoo { get; set; }

        /// <summary>
        /// 点击详细的时候查询详细的内容使用
        /// <code>
        ///     Veidoo = 0 月度, 传值表达式为 2023M2 ,表示2023年第二个月整个月份;
        ///     Veidoo = 1 季度, 传值表达式为 2023Q1 ,表示2023年第一个季度整个季度;
        ///     Veidoo = 2 年度, 传值表达式为 2023 ,表示2023年整年
        /// </code>
        /// </summary>
        public string DateDetail { get; set; }

    }

}
