using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.EventBus;

namespace YiJian.ECIS.Call
{
    /// <summary>
    /// 接诊事件
    /// </summary>
    [EventName("ECIS.Call.Department.Created")]
    public class DepartmentCreatedEvent
    {
        /// <summary>
        /// 科室 ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 科室名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        public string Code { get; set; }
    }
}
