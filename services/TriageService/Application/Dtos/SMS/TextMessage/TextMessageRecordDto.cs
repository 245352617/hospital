using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 短信记录Dto
    /// </summary>
    public class TextMessageRecordDto
    {
        /// <summary>
        /// 短信消息
        /// </summary>
        public string TextMessage { get; set; }

        /// <summary>
        /// 短信发送时间
        /// </summary>
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 发送到的手机
        /// </summary>
        public string SendToPhone { get; set; }
    }
}