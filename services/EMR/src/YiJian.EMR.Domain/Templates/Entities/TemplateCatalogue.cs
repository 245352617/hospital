using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 模板目录结构
    /// </summary>
    [Comment("模板目录结构")]
    public class TemplateCatalogue : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 模板目录结构
        /// </summary>
        private TemplateCatalogue()
        {

        }

        /// <summary>
        /// 创建目录分组(通用模板)
        /// </summary> 
        public TemplateCatalogue(
            Guid id,
            [NotNull] string title,
            string code,
            int sort,
            Guid? parentId = null,
            EClassify classify = EClassify.EMR,
            int level = 0
        )
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100);
            Code=code;
            IsFile = false;
            Sort = sort;
            TemplateType = ETemplateType.General;
            IsEnabled = true;
            Lv = level;
            ParentId = parentId;
            Classify = classify;
        }

        /// <summary>
        /// 创建目录分组(科室模板)
        /// </summary> 
        public TemplateCatalogue(
            Guid id,
            [NotNull] string title,
            string code,
            int sort,
            [NotNull] string deptCode,
            Guid inpatientWardId,
            Guid? parentId = null,
            EClassify classify = EClassify.EMR
        )
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100, minLength: 1);
            Code = code;
            IsFile = false;
            Sort = sort;
            TemplateType = ETemplateType.Department;
            DeptCode = Check.NotNullOrWhiteSpace(deptCode, nameof(deptCode), maxLength: 32);
            InpatientWardId = inpatientWardId;
            IsEnabled = true;
            Lv = 0;
            ParentId = parentId;
            Classify = classify;
        }

        /// <summary>
        /// 创建目录分组(个人模板)
        /// </summary> 
        public TemplateCatalogue(
            Guid id,
            [NotNull] string title,
            string code,
            int sort,
            [NotNull] string doctorCode,
            [NotNull] string doctorName,
            int lv = 0,
            Guid? parentId = null,
            EClassify classify = EClassify.EMR
        )
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100, minLength: 1);
            Code = code;
            IsFile = false;
            Sort = sort;
            TemplateType = ETemplateType.Personal;
            DoctorCode = Check.NotNullOrWhiteSpace(doctorCode, nameof(doctorCode), maxLength: 32);
            DoctorName = Check.NotNullOrWhiteSpace(doctorName, nameof(doctorCode), maxLength: 50);
            IsEnabled = true;
            Lv = lv;
            ParentId = parentId;
            Classify = classify; 
        }

        /// <summary>
        /// 创建目录（带电子病历模板）
        /// </summary>
        public TemplateCatalogue(
            Guid id,
            [NotNull] string title,
            string code,
            Guid? parentId,
            int lv,
            int sort,
            ETemplateType templateType,
            string deptCode,
            Guid? inpatientWardId,
            string doctorCode,
            string doctorName,
            bool isEnabled,
            Guid? originId,
            EClassify classify = EClassify.EMR 
        )
        {
            Id = id;
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100, minLength: 1);
            Code = code;
            IsFile = true;
            ParentId = parentId;
            Lv = lv;
            Sort = sort;
            TemplateType = templateType;
            DeptCode = deptCode;
            InpatientWardId = inpatientWardId;
            DoctorCode = doctorCode;
            DoctorName = doctorName;
            IsEnabled = isEnabled;
            Classify = classify;
            OriginId = originId;
        }

        /// <summary>
        /// 目录名称
        /// </summary>
        [Comment("目录名称")]
        [Required(ErrorMessage = "目录名称必填"), StringLength(200, ErrorMessage = "目录名称最大长度100字符")]
        public string Title { get; set; }


        /// <summary>
        /// 目录名称编码
        /// </summary>
        [Comment("目录名称编码")]
        [StringLength(200, ErrorMessage = "目录名称编码最大长度200字符")]
        public string Code { get; set; }

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
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary>
        [Comment("模板类型")]
        [Required(ErrorMessage = "模板类型必填")]
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>
        [Comment("科室编码")]
        [StringLength(32, ErrorMessage = "科室编码最大长度25字符")]
        public string DeptCode { get; set; } = "";

        /// <summary>
        /// 病区id
        /// </summary>
        [Comment("病区id")]
        public Guid? InpatientWardId { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary>
        [Comment("医生编码")]
        [StringLength(32, ErrorMessage = "医生编码最大长度25字符")]
        public string DoctorCode { get; set; } = "";

        /// <summary>
        /// 医生
        /// </summary>
        [Comment("医生")]
        [StringLength(50, ErrorMessage = "医生最大长度25字符")]
        public string DoctorName { get; set; } = "";

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 目录结构层级Level
        /// </summary>
        [Comment("目录结构层级Level")]
        public int Lv { get; set; }

        /// <summary>
        /// 引用的模板Id
        /// </summary>
        [Comment("引用的模板Id")]
        public Guid? CatalogueId { get; set; }

        /// <summary>
        /// 最初引入病历库的Id
        /// </summary>
        [Comment("最初引入病历库的Id")]
        public Guid? OriginId { get; set; }

        /// <summary>
        /// 最初引入病历库的名称
        /// </summary>
        [Comment("最初引入病历库的名称")]
        [StringLength(200)]
        public string CatalogueTitle { get; set; }

        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书)
        /// </summary>
        [Comment("电子文书分类(0=电子病历,1=文书)")]
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 更新目录分组
        /// </summary> 
        public void Update(
            [NotNull] string title,
            string code,
            int sort
        )
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100, minLength: 1);
            Code=code;
            Sort = sort;
        }

        /// <summary>
        /// 更新电子病历模板
        /// </summary> 
        public void Update(
            [NotNull] string title,
            string code,
            Guid parentId,
            int lv,
            int sort,
            bool isEnabled
        )
        {
            Title = Check.NotNullOrWhiteSpace(title, nameof(title), maxLength: 100, minLength: 1);
            Code = code;
            ParentId = parentId;
            Lv = lv;
            Sort = sort;
            IsEnabled = isEnabled;
        }

        /// <summary>
        /// 更新关联的病历库内容
        /// </summary>
        /// <param name="catalogueId"></param>
        /// <param name="catalogueTitle"></param>
        /// <param name="originId"></param>
        public void SetCatalogue(Guid? catalogueId, string catalogueTitle = null, Guid? originId = null)
        {
            CatalogueId = catalogueId;
            CatalogueTitle = catalogueTitle;
            if (originId.HasValue && originId != Guid.Empty) OriginId = originId;
        }

        /// <summary>
        /// 更新原始的病历库id
        /// </summary>
        /// <param name="originId"></param>
        public void SetOrigin(Guid? originId)
        {
            OriginId = originId;
        }
    }
}
