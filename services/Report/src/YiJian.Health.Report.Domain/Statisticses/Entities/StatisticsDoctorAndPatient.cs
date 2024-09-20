using Microsoft.EntityFrameworkCore;
using Volo.Abp.Domain.Entities;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 急诊科医患比视图
    /// </summary>
    [Comment("急诊科医患比视图")]
    public class StatisticsDoctorAndPatient : Entity<int>
    {

        /// <summary>
        /// 在岗医师总数
        /// </summary>
        [Comment("在岗医师总数")]
        public int DoctorTotal { get; set; }

        /// <summary>
        /// 接诊总数
        /// </summary>
        [Comment("接诊总数")]
        public int ReceptionTotal { get; set; }

    }


}
