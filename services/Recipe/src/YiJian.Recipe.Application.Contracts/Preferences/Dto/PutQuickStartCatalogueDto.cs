using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开始目录分类
    /// </summary>
    public class PutQuickStartCatalogueDto : EntityDto<Guid>
    {
        /// <summary>
        /// 目录名称
        /// </summary>
        public string Title { get; set; }
    }

}
