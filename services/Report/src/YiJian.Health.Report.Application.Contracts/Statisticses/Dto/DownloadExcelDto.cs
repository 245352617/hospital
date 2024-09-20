using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.Statisticses.Dto
{
    /// <summary>
    /// 下载Excel报表请求参数模型
    /// </summary>
    public class DownloadExcelDto
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

        /// <summary>
        /// 是否查询详细，默认否=查询列表，设为true=是 的时候，DateDetail 和DetailInfo 都需要传， 返回的是详细列表的内容
        /// </summary>
        public bool IsDetail { get; set; } = false;

        /// <summary>
        /// 点击详细的时候查询详细的内容使用
        /// <code>
        ///     Veidoo = 0 月度, 传值表达式为 2023M2 ,表示2023年第二个月整个月份;
        ///     Veidoo = 1 季度, 传值表达式为 2023Q1 ,表示2023年第一个季度整个季度;
        ///     Veidoo = 2 年度, 传值表达式为 2023 ,表示2023年整年
        /// </code>
        /// </summary>
        public string DateDetail { get; set; }

        /// <summary>
        /// 如果有详细信息的时候查询 =》 查询的详细信息 0=默认详细， 1=医师详细， 2=护士详细， 3=患者详细
        /// </summary>
        public EDetailInfo? DetailInfo { get; set; }

    }


}
