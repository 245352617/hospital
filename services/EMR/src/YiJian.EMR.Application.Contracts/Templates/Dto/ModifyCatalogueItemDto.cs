using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 新增/编辑病历分组模型
    /// </summary>
    public class ModifyCatalogueItemDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 父级Id，根级=0
        /// </summary> 
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 目录名称
        /// </summary> 
        [Required(ErrorMessage = "目录名称必填"), StringLength(200, ErrorMessage = "目录名称最大长度100字符")]
        public string Title { get; set; }

        /// <summary>
        /// 目录名称编码
        /// </summary> 
        [StringLength(200, ErrorMessage = "目录名称编码最大长度200字符")]
        public string Code { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary> 
        public int Sort { get; set; } = 0;

        /// <summary>
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary> 
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 病区id (科室模板) [如果TemplateType=1，并且该值不传，那么视为初始化科室模板目录]
        /// </summary> 
        public Guid? InpatientWardId { get; set; }

        /// <summary>
        /// 科室code
        /// </summary>
        public string DeptCode { get;set;}

        /// <summary>
        /// 科室code
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 医生Code
        /// </summary>
        public string DoctorCode { get; set; }
        
        /// <summary>
        /// 医生名称
        /// </summary>
        public string DoctorName { get; set; }

        /// <summary>
        /// 目录结构层级Level
        /// </summary> 
        public int Lv { get; set; }

        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书) 新增有效，更新无效
        /// </summary> 
        public EClassify Classify { get; set; } = EClassify.EMR;

    }

}
