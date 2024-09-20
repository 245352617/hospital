using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 我的电子病历模板[包括 通用模板，可是模板，个人模板]
    /// </summary>
    public class MyXmlTemplateDto:EntityDto<Guid>
    {
        /// <summary>
        /// xml模板
        /// </summary> 
        public string TemplateXml { get; set; }

        /// <summary>
        /// 目录结构树模板Id
        /// </summary> 
        public Guid TemplateCatalogueId { get; set; } 
    }

}
