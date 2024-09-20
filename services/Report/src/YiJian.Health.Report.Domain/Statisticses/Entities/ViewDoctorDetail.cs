using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace YiJian.Health.Report.Statisticses.Entities
{
    /// <summary>
    /// 医师详细视图
    /// </summary>
    [Comment("医师详细视图")]
    public class ViewDoctorDetail
    {
        /// <summary>
        /// 医师
        /// </summary>
        [Comment("医师")]
        [StringLength(50)]
        public string Doctor { get; set; }

        /// <summary>
        /// 接诊人数
        /// </summary>
        [Comment("接诊人数")]
        public int ReceptionTotal { get; set; }

    }


}
