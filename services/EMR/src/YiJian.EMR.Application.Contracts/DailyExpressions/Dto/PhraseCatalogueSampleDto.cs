using System.Collections.Generic;

namespace YiJian.EMR.DailyExpressions.Dto
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    public class PhraseCatalogueSampleDto : PhraseCatalogueInfoDto
    {  
        /// <summary>
        /// 常用语短语
        /// </summary>
        public List<PhraseDto> Phrases { get; set; } = new List<PhraseDto>();

    }

}
