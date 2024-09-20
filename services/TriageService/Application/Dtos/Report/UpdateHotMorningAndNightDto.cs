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
    public class UpdateHotMorningAndNightDto
    {
        /// <summary>
        /// 早八统计人数（修改）
        /// </summary>
        [Description("早八统计人数（修改）")]
        public uint? MorningCountChanged { get; set; }

        /// <summary>
        /// 晚八统计人数（修改）
        /// </summary>
        [Description("晚八统计人数（修改）")]
        public uint? NightCountChanged { get; set; }
    }
}
