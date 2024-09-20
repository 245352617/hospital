using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 通用模板目录
    /// </summary>
    [JsonConverter(typeof(GeneralCatalogueTreeJsonConverter))]
    public class GeneralCatalogueTreeDto : TemplateCatalogueBaseDto
    { 
        /// <summary>
        /// 当前目录下的目录或电子病历
        /// </summary>
        public virtual List<GeneralCatalogueTreeDto> Catalogues { get; set; } = new List<GeneralCatalogueTreeDto>();
    }

}
