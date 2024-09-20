using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 短信消息记录
    /// </summary>
    public class TextMessageRecord : Entity<Guid>
    {
        /// <summary>
        /// 短信消息
        /// </summary>
        [Description("短信消息")]
        public string TextMessage { get; set; }

        /// <summary>
        /// 短信发送时间
        /// </summary>
        [Description("短信发送时间")]
        public DateTime SendTime { get; set; }

        /// <summary>
        /// 任务单Id
        /// </summary>
        [Description("任务单Id")]
        public Guid TaskInfoId { get; set; }

        /// <summary>
        /// 任务单号
        /// </summary>
        [Description("任务单号")]
        [MaxLength(50,ErrorMessage = "任务单号的最大长度为{1}")]
        public string TaskInfoNum { get; set; }

        /// <summary>
        /// 发送到的手机
        /// </summary>
        [Description("发送到的手机号码")]
        public string SendToPhone { get; set; }

        /// <summary>
        /// 短信发送响应
        /// </summary>
        [Description("短信发送响应")]
        public string Response { get; set; }
    }
}