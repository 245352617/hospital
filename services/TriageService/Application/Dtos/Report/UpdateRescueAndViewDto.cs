using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 抢救区、留观区修改数据
    /// </summary>
    public class UpdateRescueAndViewDto
    {
        /// <summary>
        /// 统计人数（修改）
        /// </summary>
        [Description("统计人数（修改）")]
        public uint? CountChanged { get; set; }
    }
}
