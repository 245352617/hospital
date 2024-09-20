using System;
using System.Collections.Generic;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 添加快速开嘱信息
    /// </summary>
    public class AddQuickStartAdviceDto
    {
        /// <summary>
        /// 快速开嘱目录Id
        /// </summary>
        public Guid QuickStartCatalogueId { get; set; }

        public List<QuickStartSampleAdviceDto> QuickStartAdvices { get; set; }

    }

}
