using System;
using System.ComponentModel;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 短信模板
    /// </summary>
    public class TextMessageTemplate : Entity<Guid>,ISoftDelete,IHasConcurrencyStamp,IHasCreationTime,IHasModificationTime
    {
        /// <summary>
        /// 模板内容
        /// </summary>
        [Description("模板内容")]
        public string Content { get; set; }

        /// <summary>
        /// 是否删除，0=否，1=是
        /// </summary>
        [Description("是否删除，0=否，1=是")]
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 乐观锁
        /// </summary>
        [Description("乐观锁")]
        public string ConcurrencyStamp { get; set; }
        
        /// <summary>
        /// 创建时间
        /// </summary>
        [Description("创建时间")]
        public DateTime CreationTime { get; set; }
        
        /// <summary>
        /// 修改时间
        /// </summary>
        [Description("修改时间")]
        public DateTime? LastModificationTime { get; set; }
    }
}