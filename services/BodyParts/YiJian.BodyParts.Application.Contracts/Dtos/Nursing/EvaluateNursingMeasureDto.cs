using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 评价护理措施
    /// </summary>
    public class EvaluateNursingMeasureDto : EntityDto<Guid>
    {
        /// <summary>
        /// 评价内容
        /// </summary>
        public string Evaluation { get; set; }
        /// <summary>
        /// 评价人
        /// </summary>
        public string EvalNurseCode { get; set; }
        /// <summary>
        /// 评价名称
        /// </summary>
        public string EvalNurseName { get; set; }
        /// <summary>
        /// 评价时间
        /// </summary>
        public DateTime? EvalRecordTime { get; set; }
    }
}
