using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Entities
{
    /// <summary>
    /// 我的XML电子病例模板(通用模板，科室模板，个人模板)
    /// </summary>
    [Comment("被管理起来的XML电子病例模板(通用模板，科室模板，个人模板)")]
    public class MyXmlTemplate : Entity<Guid>
    {
        /// <summary>
        /// xml电子病历模板
        /// </summary>
        private MyXmlTemplate()
        {

        }

        /// <summary>
        /// xml电子病历模板
        /// </summary>
        /// <param name="id"></param>
        /// <param name="templateXml">xml病例文件</param>
        /// <param name="catalogueId">病例模板的id</param> 
        public MyXmlTemplate(
            Guid id,
            [NotNull] string templateXml,
            Guid catalogueId
        )
        {
            Id = id;
            TemplateXml = Check.NotNull(templateXml, nameof(templateXml));
            TemplateCatalogueId = catalogueId; 
        }

        /// <summary>
        /// xml模板
        /// </summary>
        [Comment("xml模板")]
        [Required(ErrorMessage = "xml模板不能为空")]
        [DataType(DataType.MultilineText)]
        public string TemplateXml { get; set; }
          
        /// <summary>
        /// 目录结构树模板Id (源目录)
        /// </summary>
        [Comment("目录结构树模板Id")]
        [Required(ErrorMessage = "必须映射对应的目录结构")]
        public Guid TemplateCatalogueId { get; set; }

        /// <summary>
        /// 电子病历模板
        /// </summary> 
        public virtual TemplateCatalogue TemplateCatalogue { get; set; }
           
        /// <summary>
        /// 更新电子病历文档
        /// </summary>
        /// <param name="xml"></param>
        public void UpdateTemplateXml(string xml)
        {
            TemplateXml = xml;
        }
    }
}
