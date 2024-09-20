using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class OrderExecTimeDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 医嘱组号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 实际执行时间
        /// </summary>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public string DrugState { get; set; }
    }
}
