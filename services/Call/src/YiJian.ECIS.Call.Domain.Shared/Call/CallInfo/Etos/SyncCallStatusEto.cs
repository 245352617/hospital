using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.ECIS.Call.CallCenter.Etos
{
    /// <summary>
    /// 对外发布叫号状态变化消息
    /// </summary>
    public class SyncCallStatusEto
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="registerNo">registerNo</param>
        /// <param name="callingSn">排队号</param>
        /// <param name="callStatus">叫号状态 0：未叫号  1：叫号中  2：已叫号</param>
        /// <param name="callTime">叫号时间</param>
        /// <param name="logDate"></param>
        /// <param name="logTime"></param>
        public SyncCallStatusEto(string registerNo, string callingSn, CallStatus callStatus, DateTime? callTime, DateTime? logDate, DateTime? logTime)
            : this(registerNo, callStatus, callTime)
        {
            this.CallingSn = callingSn;
            this.LogDate = logDate;
            this.LogTime = logTime;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="RegisterNo">患者分诊Id</param>
        /// <param name="callStatus">叫号状态 0：未叫号  1：叫号中  2：已叫号</param>
        /// <param name="callTime">叫号时间</param>
        public SyncCallStatusEto(string RegisterNo, CallStatus callStatus, DateTime? callTime)
        {
            this.RegisterNo = RegisterNo;
            this.CallStatus = callStatus;
            this.CallTime = callTime;
        }

        /// <summary>
        /// 患者分诊Id
        /// </summary>
        public string RegisterNo { get; set; }

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
