using System;
using Volo.Abp.Application.Dtos;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开嘱类型目录
    /// </summary>
    public class CataloguesDto : EntityDto<Guid>
    {
        public CataloguesDto()
        {

        }

        public CataloguesDto(Guid id, string title, bool canModify, int sort)
        {
            Id = id;
            Title = title;
            CanModify = canModify;
            Sort = sort;
        }

        /// <summary>
        /// 标题名称
        /// </summary> 
        public string Title { get; set; }

        /// <summary>
        /// 是否可以修改标题名称
        /// </summary> 
        public bool CanModify { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }
}
