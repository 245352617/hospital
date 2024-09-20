using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Recipes.Preferences.Entities
{
    /// <summary>
    /// 快速开嘱目录
    /// </summary>
    [Comment("快速开嘱目录")]
    public class QuickStartCatalogue : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 快速开嘱目录
        /// </summary>
        private QuickStartCatalogue()
        {

        }

        /// <summary>
        /// 快速开嘱目录
        /// </summary> 
        public QuickStartCatalogue(Guid id,
            string title,
            string doctorCode,
            string doctorName,
            bool canModify,
            int sort,
            EPlatformType platformType)
        {
            Id = id;
            Title = title;
            DoctorCode = doctorCode;
            DoctorName = doctorName;
            CanModify = canModify;
            Sort = sort;
            PlatformType = platformType;
        }

        /// <summary>
        /// 标题名称
        /// </summary>
        [Comment("分类名称")]
        [Required, StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 医生编号
        /// </summary>
        [Comment("医生编号")]
        [StringLength(50)]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        [Comment("医生名称")]
        [StringLength(50)]
        public string DoctorName { get; set; }

        /// <summary>
        /// 平台标识
        /// </summary>
        [Comment("平台标识")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 是否可以修改标题名称
        /// </summary>
        [Comment("平台标识")]
        public bool CanModify { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        [Comment("序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 快速开嘱药物
        /// </summary> 
        public virtual List<QuickStartAdvice> QuickStartAdvices { get; set; } = new List<QuickStartAdvice>();

        /// <summary>
        /// 修改标题
        /// </summary>
        /// <param name="title"></param>
        public void ModifyTitle([NotNull] string title)
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 50);
        }
    }
}
