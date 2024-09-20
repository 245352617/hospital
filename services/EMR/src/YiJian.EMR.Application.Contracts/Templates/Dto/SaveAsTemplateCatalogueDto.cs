using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 另存为新的电子病例的结构树
    /// </summary>
    public class SaveAsTemplateCatalogueDto
    {
        /// <summary>
        /// 目录的Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 目录标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 目录标题编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 病区id
        /// </summary> 
        public Guid? InpatientWardId { get; set; }

        /// <summary>
        /// 科室编码
        /// </summary>  
        public string DeptCode { get; set; }

        /// <summary>
        /// 医生编码
        /// </summary> 
        public string DoctorCode { get; set; }

        /// <summary>
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary> 
        public ETemplateType TemplateType { get; set; }

    }

}
