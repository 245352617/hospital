using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 值班电话
    /// </summary>
    public class DutyTelephone : FullAuditedAggregateRoot<Guid>
    {
        public DutyTelephone()
        {
            
        }

        public DutyTelephone(Guid id)
        {
            Id = id;
        }
        
        
        /// <summary>
        /// 序号
        /// </summary>
        [Description("序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [Description("科室编码")]
        [MaxLength(50,ErrorMessage = "科室编码的最大长度为{1}")]
        public string Code { get; set; }
        
        /// <summary>
        /// 科室名称
        /// </summary>
        [Description("科室名称")]
        [MaxLength(50,ErrorMessage = "科室名称的最大长度为{1}")]
        public string Dept { get; set; }

        /// <summary>
        /// 手机编号
        /// </summary>
        [Description("手机编号")]
        [MaxLength(20,ErrorMessage = "手机编号的最大长度为{1}")]
        public string No { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [Description("手机号码")]
        [MaxLength(20,ErrorMessage = "手机号码的最大长度为{1}")]
        public string Telephone { get; set; }

        /// <summary>
        /// 关联绿通
        /// </summary>
        [Description("关联绿通")]
        [MaxLength(200,ErrorMessage = "关联绿通的最大长度为{1}")]
        public string GreenRoads { get; set; }

        /// <summary>
        /// 是否开启绿通消息
        /// </summary>
        [Description("是否开启绿通消息")]
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 标签设置Id
        /// </summary>
        [Description("标签设置Id")]
        public Guid TagSettingId { get; set; }
        
        
    }
    
    
}