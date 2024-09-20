using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Dtos
{
    public class CallingStatisticsDto
    {
        /// <summary>
        /// 门诊医生数量
        /// </summary>
        public long DoctorCount { get; set; }

        /// <summary>
        /// 叫号总数
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 候诊人数
        /// </summary>
        public int WattingCount { get; set; }

        /// <summary>
        /// 已就诊人数
        /// </summary>
        public int TreatedCount { get; set; }

        /// <summary>
        /// 过号人数
        /// </summary>
        public int UntreatedOverCount { get; set; }

        /// <summary>
        /// 科室叫号统计
        /// </summary>
        public List<DeptCallingStatisticsDto> DeptCallings { get; set; } = new();
    }
}
