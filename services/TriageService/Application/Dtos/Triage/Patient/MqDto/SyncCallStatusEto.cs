using SamJan.MicroService.TriageService.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.TriageService.MqDto
{
    /// <summary>
    /// 对外发布叫号状态变化消息
    /// </summary>
    public class SyncCallStatusEto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pI_ID">患者分诊Id</param>
        /// <param name="callingSn">排队号</param>
        /// <param name="callStatus">叫号状态 0：未叫号  1：叫号中  2：已叫号</param>
        /// <param name="callTime">叫号时间</param>
        /// <param name="logDate"></param>
        /// <param name="logTime"></param>
        public SyncCallStatusEto(Guid pI_ID, string callingSn, CallStatus callStatus, DateTime? callTime, DateTime? logDate, DateTime? logTime)
            : this(pI_ID, callStatus, callTime)
        {
            this.CallingSn = callingSn;
            this.LogDate = logDate;
            this.LogTime = logTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pI_ID">患者分诊Id</param>
        /// <param name="callStatus">叫号状态 0：未叫号  1：叫号中  2：已叫号</param>
        /// <param name="callTime">叫号时间</param>
        public SyncCallStatusEto(Guid pI_ID, CallStatus callStatus, DateTime? callTime)
        {
            this.PI_ID = pI_ID;
            this.CallStatus = callStatus;
            this.CallTime = callTime;
        }

        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 排队号
        /// </summary>
        public string CallingSn { get; set; }

        /// <summary>
        /// 叫号状态 0：未叫号  1：叫号中  2：已叫号
        /// </summary>
        public CallStatus CallStatus { get; set; }

        /// <summary>
        /// 叫号时间
        /// </summary>
        public DateTime? CallTime { get; set; }

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
