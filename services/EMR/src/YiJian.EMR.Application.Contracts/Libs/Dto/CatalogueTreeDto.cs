using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 目录树
    /// </summary> 
    [JsonConverter(typeof(CatalogueTreeJsonConverter))]
    public class CatalogueTreeDto : CatalogueDto
    {
        /// <summary>
        /// 当前目录下的目录或电子病历
        /// </summary>
        public virtual List<CatalogueTreeDto> Catalogues { get;set; } = new List<CatalogueTreeDto>(); 

    }

}
