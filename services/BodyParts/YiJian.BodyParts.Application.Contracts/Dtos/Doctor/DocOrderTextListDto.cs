using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 医嘱列表Dto
    /// </summary>
    public class DocOrderTextListDto
    {
        /// <summary>
        /// 组号
        /// </summary>
        public string GroupNo { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string OrderText { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime NurseTime { get; set; }
    }
}
