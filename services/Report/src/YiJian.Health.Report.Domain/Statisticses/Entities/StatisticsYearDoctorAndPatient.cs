using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科医患年度视图
    /// </summary>
    [Comment("急诊科医患年度视图")]
    public class StatisticsYearDoctorAndPatient : StatisticsDoctorAndPatient
    {

        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }
    }


}
