using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科各级患者比例视图
    /// </summary>
    [Comment("急诊科各级患者比例视图")]
    public class StatisticsLevelAndPatient:Entity<int>
    {
        /// <summary>
        /// I级
        /// </summary>
        [Comment("I级")]
        public int LI { get; set; }

        /// <summary>
        /// II级
        /// </summary>
        [Comment("II级")]
        public int LII { get; set; }

        /// <summary>
        /// III级
        /// </summary>
        [Comment("III级")]
        public int LIII { get; set; }

        /// <summary>
        /// IVa级
        /// </summary>
        [Comment("IVa级")]
        public int LIVa { get; set; }

        /// <summary>
        /// IVb级
        /// </summary>
        [Comment("IVb级")]
        public int LIVb { get; set; }
         
    }
}
