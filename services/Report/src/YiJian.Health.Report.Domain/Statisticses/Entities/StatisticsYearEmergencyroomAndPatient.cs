using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 抢救室滞留时间中位数年度视图
    /// </summary>
    [Comment("抢救室滞留时间中位数年度视图")]
    public class StatisticsYearEmergencyroomAndPatient : StatisticsEmergencyroomAndPatient
    {
        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }
    }
}
