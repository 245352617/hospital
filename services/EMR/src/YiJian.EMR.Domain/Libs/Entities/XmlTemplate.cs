using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Libs.Entities
{
    /// <summary>
    /// xml 病历模板
    /// </summary>
    [Comment("xml病历模板")]
    public class XmlTemplate : Entity<Guid>, ISoftDelete
    {
        /// <summary>
        /// xml 病历模板
        /// </summary>
        private XmlTemplate()
        {

        }

        /// <summary>
        /// xml 病历模板
        /// </summary>
        public XmlTemplate(
            Guid id,
            [NotNull] string templateXml,
            Guid catalogueId,
            bool isDeleted = false
        )
        {
            Id = id;
            TemplateXml = Check.NotNull(templateXml, nameof(templateXml));
            CatalogueId = catalogueId;
            IsDeleted = isDeleted;
        }

        /// <summary>
        /// xml模板
        /// </summary>
        [Comment("xml模板")]
        [Required(ErrorMessage = "xml模板不能为空")]
        [DataType(DataType.MultilineText)]
        public string TemplateXml { get; set; }

        /// <summary>
        /// 目录结构树模板Id
        /// </summary>
        [Comment("目录结构树模板Id")]
        [Required(ErrorMessage = "必须映射对应的目录结构")]
        public Guid CatalogueId { get; set; }

        /// <summary>
        /// 目录结构树模板
        /// </summary>  
        public virtual Catalogue Catalogue { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

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
