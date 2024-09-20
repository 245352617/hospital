using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;

namespace YiJian.Health.Report.Statisticses.Entities
{

    /// <summary>
    /// 护士详细视图
    /// </summary>
    [Comment("护士详细视图")]
    public class ViewNurseDetail
    {
        /// <summary>
        /// 护士
        /// </summary>
        [Comment("护士")]
        public string Nurse { get; set; }

        /// <summary>
        /// 接诊人数
        /// </summary>
        [Comment("接诊人数")]
        public int ReceptionTotal { get; set; }

    }
    

}
