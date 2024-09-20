using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 抢救室滞留时间中位数视图
    /// </summary>
    [Comment("抢救室滞留时间中位数视图")]
    public class StatisticsEmergencyroomAndPatient:Entity<int>
    {
        /// <summary>
        /// 抢救总数
        /// </summary>
        [Comment("抢救总数")]
        public int RescueTotal { get; set; }

        /// <summary>
        /// 平均滞留时间
        /// </summary>
        [Comment("平均滞留时间")]
        public int AvgDetentionTime { get; set; }

        /// <summary>
        /// 滞留时间中位数
        /// </summary>
        [Comment("滞留时间中位数")]
        public int MidDetentionTime { get; set; }

    }
}
