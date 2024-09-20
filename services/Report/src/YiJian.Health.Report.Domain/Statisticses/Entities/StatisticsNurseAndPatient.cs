using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科护患比视图
    /// </summary>
    [Comment("急诊科护患比视图")]
    public class StatisticsNurseAndPatient:Entity<int>
    {
        /// <summary>
        /// 在岗护士总数
        /// </summary>
        [Comment("在岗护士总数")]
        public int NurseTotal { get; set; }

        /// <summary>
        /// 接诊总数
        /// </summary>
        [Comment("接诊总数")]
        public int ReceptionTotal { get; set; }
         
    }
    

}
