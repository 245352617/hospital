using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 新增/编辑病历模型
    /// </summary>
    public class ModifyEmrItemDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 目录名称
        /// </summary> 
        [Required(ErrorMessage = "病历名称必填"), StringLength(200, ErrorMessage = "病历名称最大长度100字符")]
        public string Title { get; set; }

        /// <summary>
        /// 目录名称编码
        /// </summary> 
        [StringLength(200, ErrorMessage = "病历名称编码最大长度200字符")]
        public string Code { get; set; }

        /// <summary>
        /// 父级Id,不传就是根
        /// </summary>  
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary> 
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary> 
        [Required]
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary> 
        public bool IsEnabled { get; set; } = false;

        /// <summary>
        /// 电子病历库中电子病例Id
        /// </summary>
        [Required(ErrorMessage = "必须选择一个电子病历模板")]
        public Guid CatalogueId { get; set; }

        /// <summary>
        /// 最初引入病历库的Id
        /// </summary>
        [Required(ErrorMessage = "必须传入病历库的原始ID")]
        public Guid OriginId { get; set; }

        /// <summary>
        /// 医生编码 [编辑个人模板的时候需要]
        /// </summary>
        public string DoctorCode { get; set; } = "";

        /// <summary>
        /// 医生名称 [编辑个人模板的时候需要]
        /// </summary>
        public string DoctorName { get; set; } = "";

        /// <summary>
        /// 科室编码 [编辑科室模板的时候需要]
        /// </summary>
        public string DeptCode { get; set; } = "";

        /// <summary>
        /// 病区id  [编辑科室模板的时候需要]
        /// </summary> 
        public Guid? InpatientWardId { get; set; }

        /// <summary>
        /// 目录结构层级Level
        /// </summary> 
        public int Lv { get; set; }

        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书) ,新增是有效，更新时无效
        /// </summary> 
        public EClassify Classify { get; set; } = EClassify.EMR;
    }
}
