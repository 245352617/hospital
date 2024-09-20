using Microsoft.EntityFrameworkCore;
using System;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科医患月度视图
    /// </summary>
    [Comment("急诊科医患月度视图")]
    public class StatisticsMonthDoctorAndPatient : StatisticsDoctorAndPatient
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
        /// 年
        /// </summary>
        [Comment("年")]
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        [Comment("月")]
        public int Month { get; set; }
        
    }


}
