using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊抢救室患者死亡率年度视图
    /// </summary>
    [Comment("急诊抢救室患者死亡率年度视图")]
    public class StatisticsYearEmergencyroomAndDeathPatient : StatisticsEmergencyroomAndDeathPatient
    {

        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }
    }

}
