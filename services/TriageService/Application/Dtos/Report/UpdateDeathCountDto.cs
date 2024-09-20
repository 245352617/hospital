using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 早八晚八修改数据
    /// </summary>
    public class UpdateDeathCountDto
    {
        /// <summary>
        /// 统计人数（修改）
        /// </summary>
        [Description("统计人数（修改）")]
        public uint? DeathCountChanged { get; set; }
    }
}
