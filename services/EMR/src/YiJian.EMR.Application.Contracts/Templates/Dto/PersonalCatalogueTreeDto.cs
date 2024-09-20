using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Templates.Dto
{ 
    /// <summary>
    /// 个人模板
    /// </summary>
    [JsonConverter(typeof(PersonalCatalogueTreeJsonConverter))]
    public class PersonalCatalogueTreeDto : PersonalCatalogueListDto
    {
        /// <summary>
        /// 当前目录下的目录或电子病历
        /// </summary>
        public virtual List<PersonalCatalogueTreeDto> Catalogues { get; set; } = new List<PersonalCatalogueTreeDto>();
    }
     
}
