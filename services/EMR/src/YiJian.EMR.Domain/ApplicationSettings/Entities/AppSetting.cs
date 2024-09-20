using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities; 

namespace YiJian.EMR.ApplicationSettings.Entities
{
    /// <summary>
    /// 应用配置
    /// </summary>
    [Comment("应用配置")]
    public class AppSetting : Entity<Guid>
    {
        private AppSetting()
        {

        }
        /// <summary>
        /// 应用配置
        /// </summary> 
        public AppSetting([Required]Guid id, [NotNull]string name, [NotNull] string data)
        {
            Id = id;
            Name = Check.NotNullOrEmpty( name,nameof(name),maxLength:50);
            Data = Check.NotNullOrEmpty( data,nameof(data),maxLength:500);
        }

        /// <summary>
        /// 配置名称(避开关键字key)
        /// </summary>
        [Comment("配置名称")]
        [Required(ErrorMessage ="配置键必填"),StringLength(50,ErrorMessage ="配置键最长50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 配置值(避开关键字value)
        /// </summary>
        [Comment("配置值")]
        [Required(ErrorMessage = "配置值必填"), StringLength(200, ErrorMessage = "配置键最长200个字符")]
        public string Data { get; set; }

    }
}
