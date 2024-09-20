using Microsoft.EntityFrameworkCore;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科护患年度视图
    /// </summary>
    [Comment("急诊科护患年度视图")]
    public class StatisticsYearNurseAndPatient : StatisticsNurseAndPatient
    {

        /// <summary>
        /// 年份
        /// </summary>
        [Comment("年份")]
        public int Year { get; set; }
    }
    

}
