using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 叫号排队号生成事件
    /// </summary>
    public class CallingSnGeneratedMessageDto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="triageId"></param>
        /// <param name="callingSn"></param>
        /// <param name="logDate"></param>
        /// <param name="logTime"></param>
        public CallingSnGeneratedMessageDto(Guid triageId, string callingSn, DateTime? logDate, DateTime? logTime)
        {
            this.PI_ID = triageId;
            this.CallingSn = callingSn;
            this.LogDate = logDate;
            this.LogTime = logTime;
        }

        /// <summary>
        /// 病患分诊记录ID
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 叫号排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 排队日期
        /// </summary>
        public DateTime? LogDate { get; set; }

        /// <summary>
        /// 排队时间
        /// </summary>
        public DateTime? LogTime { get; set; }
    }
}
