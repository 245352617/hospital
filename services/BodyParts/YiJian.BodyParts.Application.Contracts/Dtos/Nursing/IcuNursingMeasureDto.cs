using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 护理措施Dto
    /// </summary>
    public class IcuNursingMeasureDto : CreateNursingMeasure
    {
        /// <summary>
        /// 结束人
        /// </summary>
        public string EndCode { get; set; }
        /// <summary>
        /// 结束人名称
        /// </summary>
        public string EndName { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
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
