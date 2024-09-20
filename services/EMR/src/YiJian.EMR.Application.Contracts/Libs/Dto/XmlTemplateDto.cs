using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 电子病历模板
    /// </summary>
    public class XmlTemplateDto:EntityDto<Guid>
    {
        /// <summary>
        /// xml模板
        /// </summary> 
        public string TemplateXml { get; set; }

        /// <summary>
        /// 目录结构树模板Id
        /// </summary> 
        public Guid CatalogueId { get; set; }
    }
}
