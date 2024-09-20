using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.QualityControl
{

    public class PT_GetQcReq
    {
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
    }
}