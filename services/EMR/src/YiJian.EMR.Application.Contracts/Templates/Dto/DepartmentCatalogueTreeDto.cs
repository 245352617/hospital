using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Templates.Dto
{

    /// <summary>
    /// 科室模板
    /// </summary>
    [JsonConverter(typeof(DepartmentCatalogueTreeJsonConverter))]
    public class DepartmentCatalogueTreeDto : DepartmentCatalogueDto
    {
        /// <summary>
        /// 当前目录下的目录或电子病历
        /// </summary>
        public virtual List<DepartmentCatalogueTreeDto> Catalogues { get; set; } = new List<DepartmentCatalogueTreeDto>();
    }


}
