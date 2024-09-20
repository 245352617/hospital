using System;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 科室模板
    /// </summary>
    public class DepartmentCatalogueDto : TemplateCatalogueBaseDto
    { 
        /// <summary>
        /// 科室编码
        /// </summary> 
        public string DeptCode { get; set; } 

        /// <summary>
        /// 科室编码
        /// </summary> 
        public string DeptName { get; set; } 

        /// <summary>
        /// 病区id
        /// </summary> 
        public Guid? InpatientWardId { get; set; }

        /// <summary>
        /// 病区名称
        /// </summary> 
        public string WardName { get; set; }
    }


}
