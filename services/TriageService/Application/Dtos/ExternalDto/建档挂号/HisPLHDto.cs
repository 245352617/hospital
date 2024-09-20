using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.TriageService.Application.Dtos
{
    /// <summary>
    /// HIS 排队接口返回
    /// </summary>
    public class HisPLHDto
    {
        /// <summary>
        /// 入队 ID
        /// </summary>
        public string rdid { get; set; }

        public string jzxh { get; set; }

        /// <summary>
        /// 排队号码
        /// </summary>
        public string pdhm { get; set; }

        /// <summary>
        /// 屏幕显示的排队号码
        /// </summary>
        public string jlxh { get; set; }

        public string error { get; set; }
    }
}
