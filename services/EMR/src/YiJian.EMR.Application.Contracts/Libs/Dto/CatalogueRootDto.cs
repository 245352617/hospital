using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 目录树跟分组
    /// </summary>
    [JsonConverter(typeof(CatalogueRootJsonConverter))]
    public class CatalogueRootDto : EntityDto<Guid>
    {
        /// <summary>
        /// 目录名称
        /// </summary>  
        public string Title { get; set; }

        /// <summary>
        /// 父级Id，根级=0
        /// </summary>  
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get;set;}

        /// <summary>
        /// 目录子节点
        /// </summary>
        public virtual List<CatalogueRootDto> Catalogues { get;set; } = new List<CatalogueRootDto>();
    }

}
