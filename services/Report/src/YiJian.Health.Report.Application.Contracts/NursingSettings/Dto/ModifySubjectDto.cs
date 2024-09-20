using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.NursingSettings.Dto
{
    /// <summary>
    /// 新增/更新护理单主题内容
    /// </summary>
    public class ModifySubjectDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 配置组Id
        /// </summary>
        [Comment("配置组Id")]
        [Required, StringLength(50, ErrorMessage = "配置组名称需在50字内")]
        public string GroupId { get; set; }

        /// <summary>
        /// 配置组名称
        /// </summary>
        [Comment("配置组名称")]
        public string GroupName { get; set; }

        /// <summary>
        /// 表头分类
        /// </summary> 
        [Required, StringLength(50, ErrorMessage = "表头分类内容需在50字内")]
        public string Category { get; set; }

        /// <summary>
        /// 排序顺序
        /// </summary> 
        public int Sort { get; set; } = 0;

    }
}
