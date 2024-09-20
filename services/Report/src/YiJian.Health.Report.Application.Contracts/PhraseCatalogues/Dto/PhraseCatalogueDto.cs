using System.Collections.Generic;
using YiJian.Health.Report.Enums;

namespace YiJian.Report.PhraseCatalogues.Dto
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    public class PhraseCatalogueDto
    {
        /// <summary>
        /// 模板类型，0=通用(全院)，1=科室，2=个人
        /// </summary>
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 常用语目录
        /// </summary>
        public List<PhraseCatalogueInfoDto> Catalogues { get; set; } = new List<PhraseCatalogueInfoDto>();
    }


}
