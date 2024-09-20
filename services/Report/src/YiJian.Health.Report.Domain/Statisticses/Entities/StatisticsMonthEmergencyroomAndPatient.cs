using Microsoft.EntityFrameworkCore;
using System;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 抢救室滞留时间中位数月度视图
    /// </summary>
    [Comment("抢救室滞留时间中位数月度视图")]
    public class StatisticsMonthEmergencyroomAndPatient : StatisticsEmergencyroomAndPatient
    {
        /// <summary>
        /// 月份，方便查询用的字段
        /// <![CDATA[
        ///  2023-10-01 表示 2023年10月份的记录
        /// ]]>
        /// </summary>
        [Comment("月份，方便查询用的字段")]
        public DateTime YearMonth { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        [Comment("月份")]
        public int Month { get; set; }

    }
}
