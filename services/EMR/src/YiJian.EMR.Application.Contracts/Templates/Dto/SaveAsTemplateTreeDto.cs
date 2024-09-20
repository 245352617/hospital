using System.Collections.Generic;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 另存为新的电子病例的结构树
    /// </summary>
    public class SaveAsTemplateTreeDto
    { 
        /// <summary>
        /// 通用模板目录
        /// </summary> 
        public List<SaveAsTemplateCatalogueDto> General { get; set; } = new List<SaveAsTemplateCatalogueDto>();
         
        /// <summary>
        /// 科室模板目录
        /// </summary>

        public List<SaveAsTemplateCatalogueDto> Department { get; set; } = new List<SaveAsTemplateCatalogueDto>();

        /// <summary>
        /// 个人模板目录
        /// </summary>
        public List<SaveAsTemplateCatalogueDto> Personal { get; set; } = new List<SaveAsTemplateCatalogueDto>(); 
   
    }

}
