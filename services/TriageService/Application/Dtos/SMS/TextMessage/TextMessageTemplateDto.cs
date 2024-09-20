using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 短信模板Dto
    /// </summary>
    public class TextMessageTemplateDto
    {
        /// <summary>
        /// Id（新增时不传入）
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        /// 模板内容
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        public string ConcurrencyStamp { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime Time { get; set; }
    }
}