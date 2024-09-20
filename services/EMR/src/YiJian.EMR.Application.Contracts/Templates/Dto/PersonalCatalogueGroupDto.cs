using System;
using System.Collections.Generic;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Templates.Dto
{
    /// <summary>
    /// 根据医生分组
    /// </summary>
    public class PersonalCatalogueGroupDto : PersonalCatalogueListDto
    {  
        /// <summary>
        /// 医生电子病历模板目录
        /// </summary>
        public List<PersonalCatalogueTreeDto> Catalogues { get; set; } = new List<PersonalCatalogueTreeDto>();

        /// <summary>
        /// 更新数据
        /// </summary> 
        public void Update(
            Guid id, 
            string title,
            string code,
            Guid? parentId,
            ETemplateType templateType,
            bool isEnabled,
            bool isFile,
            int lv, 
            List<PersonalCatalogueTreeDto> catalogues)
        {
            Id = id; 
            Title = title;
            Code = code;
            ParentId = parentId;
            TemplateType = templateType;
            IsEnabled = isEnabled;
            IsFile = isFile;
            Lv = lv; 
            Catalogues = catalogues; 
        }
    }


}
