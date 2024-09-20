using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 获取EChart报表的请求参数
    /// </summary>
    public class EChartDataRequestDto
    {
        /// <summary>
        /// 报表类型  0=急诊科医患比, 1=急诊科护患比, 2=急诊科各级患者比例, 3=抢救室滞留时间中位数, 4=急诊抢救室患者死亡率
        /// </summary>
        public EReportType ReportType { get; set; }

        /// <summary>
        /// 维度 0=月度,1=季度,2=年度
        /// </summary>
        public EVeidoo Veidoo { get; set; }

        /// <summary>
        /// 开始时间
        /// <code>
        ///     Veidoo = 0 月度, 传值表达式为 2023M2 ,表示2023年第二个月开始;
        ///     Veidoo = 1 季度, 传值表达式为 2023Q1 ,表示2023年第一个季度开始;
        ///     Veidoo = 2 年度, 传值表达式为 2023 ,表示2023年开始
        /// </code> 
        /// </summary>
        public string BeginDate { get; set; }

        /// <summary>
        /// 结束年份 
        /// <code>
        ///     Veidoo = 0 月度, 传值表达式为 2023M2 ,表示2023年第二个月结束;
        ///     Veidoo = 1 季度, 传值表达式为 2023Q1 ,表示2023年第一个季度结束;
        ///     Veidoo = 2 年度, 传值表达式为 2023 ,表示2023年结束
        /// </code> 
        /// </summary>
        public string EndDate { get; set; }


    }

}
