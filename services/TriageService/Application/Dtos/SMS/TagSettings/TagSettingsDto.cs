using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 标签管理Dto
    /// </summary>
    public class TagSettingsDto
    {
        /// <summary>
        /// Id（新增时不传）
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 标签名称
        /// </summary>
        [MaxLength(50, ErrorMessage = "标签名称的最大长度为{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        public bool IsSendMessage { get; set; }
    }
}