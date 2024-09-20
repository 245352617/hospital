using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 更新我的电子病历模板[包括 通用模板，可是模板，个人模板]
    /// </summary>
    public class UpdateMyXmlTemplateDto : EntityDto<Guid>
    {
        /// <summary>
        /// xml模板
        /// </summary> 
        public string TemplateXml { get; set; }
          
        /// <summary>
        /// 模板类型 模板类型，0=通用，1=科室，2=个人
        /// </summary> 
        [Required(ErrorMessage = "模板类型 模板类型，0=通用，1=科室，2=个人")]
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 医生编码 [编辑个人模板的时候需要]
        /// </summary>
        public string DoctorCode { get; set; } = "";
          
        /// <summary>
        /// 科室编码 [编辑科室模板的时候需要]
        /// </summary>
        public string DeptCode { get; set; } = "";
           
        /// <summary>
        /// 电子文书分类(0=电子病历,1=文书) ,新增是有效，更新时无效
        /// </summary> 
        public EClassify Classify { get; set; } = EClassify.EMR;

    }

}
