using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Libs.Entities
{
    /// <summary>
    /// 电子病历库目录树（和模板分离，统一导入模板入库入口）
    /// </summary>
    [Comment("电子病历库目录树")]
    public class Catalogue : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 电子病历目录树
        /// </summary>
        private Catalogue()
        {

        }

        /// <summary>
        /// 电子病历目录树
        /// </summary>
        public Catalogue(
            Guid id,
            [NotNull] string title,
            bool isFile,
            int lv,
            Guid parentId,
            EClassify classify = EClassify.EMR
        )
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100);
            IsFile = isFile;
            Lv = lv;
            ParentId = parentId; 
            Classify = classify;
        }

        /// <summary>
        /// 目录名称
        /// </summary>
        [Comment("目录名称")]
        [Required(ErrorMessage = "目录名称必填"), StringLength(200, ErrorMessage = "目录名称最大长度100字符")]
        public string Title { get; set; }

        /// <summary>
        /// 是否是文件（文件夹=false,文件=true）
        /// </summary>
        [Comment("是否是文件（文件夹=false,文件=true）")]
        [Required]
        public bool IsFile { get; set; } = false;

        /// <summary>
        /// 父级Id，根级=0
        /// </summary>
        [Comment("父级Id，根级=0")] 
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary>
        [Comment("排序权重")]
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 目录结构层级Level
        /// </summary>
        [Comment("目录结构层级Level")]
        public int Lv { get;set;}

        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书)
        /// </summary>
        [Comment("电子文书分类(0=电子病历,1=文书)")]
        public EClassify Classify { get; set; }

        /// <summary>
        /// 更新电子病例目录操作
        /// </summary> 
        public void Update(
            [NotNull] string title,
            bool isFile,  
            int sort, 
            int lv,
            Guid? parentId = null
        )
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100);
            IsFile = isFile;
            Lv = lv;
            if (parentId.HasValue)
            {
                ParentId = parentId;
            } 
            Sort = sort; 
        }

        /// <summary>
        /// 更新目录树
        /// </summary>
        public void ModifyTitle(
            [NotNull] string title
        )
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100); 
        }

        /// <summary>
        /// 更新目录排序
        /// </summary>
        public void ModifySort(
            int sort,
            Guid modifierId
        )
        {
            Sort = sort; 
        }
         
    }
}
