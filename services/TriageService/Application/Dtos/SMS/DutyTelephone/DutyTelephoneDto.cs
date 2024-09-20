using System;
using System.ComponentModel.DataAnnotations;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 值班电话管理Dto
    /// </summary>
    public class DutyTelephoneDto
    {
        /// <summary>
        /// Id（新增时不传）
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 序号
        /// </summary>
        public int Sort { get; set; }
        
        /// <summary>
        /// 科室编码
        /// </summary>
        [MaxLength(50,ErrorMessage = "科室编码的最大长度为{1}")]
        public string Code { get; set; }
        
        /// <summary>
        /// 科室名称
        /// </summary>
        [MaxLength(50,ErrorMessage = "科室名称的最大长度为{1}")]
        public string Dept { get; set; }

        /// <summary>
        /// 手机编号
        /// </summary>
        [MaxLength(20,ErrorMessage = "手机编号的最大长度为{1}")]
        public string No { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [MaxLength(20,ErrorMessage = "手机号码的最大长度为{1}")]
        public string Telephone { get; set; }

        /// <summary>
        /// 关联绿通
        /// </summary>
        [MaxLength(200,ErrorMessage = "关联绿通的最大长度为{1}")]
        public string GreenRoads { get; set; }

        /// <summary>
        /// 是否开启绿通消息
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// 标签设置Id
        /// </summary>
        public Guid TagSettingId { get; set; }

        /// <summary>
        /// 标签管理名称
        /// </summary>
        public string TagSettingsName { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime? Time { get; set; }
    }
}