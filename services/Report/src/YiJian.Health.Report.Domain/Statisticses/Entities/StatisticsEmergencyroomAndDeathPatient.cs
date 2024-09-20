using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊抢救室患者死亡率视图
    /// </summary>
    [Comment("急诊抢救室患者死亡率视图")]
    public class StatisticsEmergencyroomAndDeathPatient : Entity<int>
    {
        /// <summary>
        /// 抢救总数
        /// </summary>
        [Comment("抢救总数")]
        public int RescueTotal { get; set; }

        /// <summary>
        /// 死亡总数
        /// </summary>
        [Comment("死亡总数")]
        public int DeathToll { get; set; }
        
    }

}
