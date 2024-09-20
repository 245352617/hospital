using System;

namespace YiJian.Recipe
{
    public class OperationApplyInput
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 申请人id
        /// </summary>
        public string ApplicantId { get; set; }
    }
}