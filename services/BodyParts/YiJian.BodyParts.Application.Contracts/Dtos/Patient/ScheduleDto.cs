using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 当前班次
    /// </summary>
    public class ScheduleDto
    {
        /// <summary>
        /// 班次日期
        /// </summary>
        public string ScheduleTime { get; set; }

        /// <summary>
        /// 班次代码
        /// </summary>
        public string ScheduleCode { get; set; }


        /// <summary>
        /// 开始班次日期
        /// </summary>
        public string StartScheduleTime { get; set; }

        /// <summary>
        /// 结束班次日期
        /// </summary>
        public string EndScheduleTime { get; set; }

        public List<IcuDeptScheduleDto> IcuDeptScheduleDto { get; set; }
    }
}
