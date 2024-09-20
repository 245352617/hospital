using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 医嘱执行--医嘱列表--计划执行列表
    /// </summary>
    public class PlanExcuteTimeListDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        /// <example></example>
        public string ExcuteFlag { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        /// <example></example>
        public DateTime? PlanExcuteTime { get; set; }
    }
}
