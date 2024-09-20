using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.Health.Report.NursingSettings.Entities
{
    /// <summary>
    /// 护理单配置
    /// </summary>
    [Comment("护理单配置")]
    public class NursingSetting : FullAuditedAggregateRoot<Guid>
    {
        public NursingSetting(Guid id, string category, int sort, string groupId, string groupName)
        {
            Id = id;
            GroupId = groupId;
            GroupName = groupName;
            Category = category;
            Sort = sort;
        }

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
        [Comment("表头分类")]
        [Required,StringLength(50,ErrorMessage = "表头分类内容需在50字内")]
        public string Category { get; set; }

        /// <summary>
        /// 排序顺序[序号]
        /// </summary>
        [Comment("排序顺序")]
        public int Sort { get; set; }

        /// <summary>
        /// 护理单配置
        /// </summary>
        public List<NursingSettingHeader> Headers { get;set;}

        public void Update(string category, int sort)
        {
            Category = category;
            Sort = sort;
        }

    }
}
