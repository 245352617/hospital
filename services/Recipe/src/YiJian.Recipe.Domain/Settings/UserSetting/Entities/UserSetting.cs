using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Recipe
{
    /// <summary>
    /// 用户个人设置
    /// </summary>
    [Comment("用户个人设置")]
    public class UserSetting : CreationAuditedEntity<Guid>
    {
        /// <summary>
        /// 用户名称
        /// </summary>
        [Required]
        [Comment("用户名称")]
        public string UserName { get; set; }

        /// <summary>
        /// 配置组
        /// </summary>
        [Comment("配置组")]
        [StringLength(50)]
        public string GroupName { get; set; }

        /// <summary>
        /// 配置组编码
        /// </summary>
        [Comment("配置组编码")]
        [StringLength(50)]
        public string GroupCode { get; set; }

        /// <summary>
        /// 配置名称
        /// </summary>
        [Required]
        [Comment("配置名称")]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 配置编码
        /// </summary>
        [Required]
        [Comment("配置编码")]
        [StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        [Required]
        [Comment("配置类型")]
        public int Type { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Required]
        [Comment("配置值")]
        [StringLength(500)]
        public string Value { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [Comment("备注")]
        [StringLength(100)]
        public string Remark { get; set; }

    }
}