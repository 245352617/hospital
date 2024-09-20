using System;
using System.Collections.Generic;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    public class DepartmentCatalogueGroupDto : DepartmentCatalogueDto
    {
        public DepartmentCatalogueGroupDto(
            Guid id,
            string title,
            string code,
            bool isFile,
            Guid? parentId,
            ETemplateType templateType,
            bool isEnabled,
            int sort,
            int lv,
            string deptCode,
            string deptName,
            Guid? inpatientWardId,
            Guid? originId)
        {
            Id = id;
            Title = title;
            Code = code;
            IsFile = isFile;
            ParentId = parentId;
            TemplateType = templateType;
            IsEnabled = isEnabled;
            Sort = sort;
            Lv = lv;
            DeptCode = deptCode;
            DeptName = deptName;
            InpatientWardId = inpatientWardId;  
            OriginId = originId;
        }

        /// <summary>
        /// 科室模板目录
        /// </summary>
        public List<DepartmentCatalogueTreeDto> Catalogues { get; set; } = new List<DepartmentCatalogueTreeDto>();
         
    }


}
