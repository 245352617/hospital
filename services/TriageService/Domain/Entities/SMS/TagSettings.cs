using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 标签管理
    /// </summary>
    public class TagSettings : Entity<Guid>
    {
        public TagSettings()
        {
            
        }
        
        public TagSettings(Guid id)
        {
            Id = id;
        }
        
        /// <summary>
        /// 标签名称
        /// </summary>
        [Description("标签名称")]
        [MaxLength(50, ErrorMessage = "标签名称的最大长度为{1}")]
        public string Name { get; set; }

        /// <summary>
        /// 是否发送短信
        /// </summary>
        [Description("是否发送短信")]
        public bool IsSendMessage { get; set; }
    }
}