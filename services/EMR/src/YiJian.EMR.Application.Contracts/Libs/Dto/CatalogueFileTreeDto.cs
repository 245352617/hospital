using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Libs.Dto
{
    /// <summary>
    /// 含有xml电子病历的结构树
    /// </summary> 
    [JsonConverter(typeof(CatalogueFileTreeJsonConverter))]
    public class CatalogueFileTreeDto : EntityDto<Guid>
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
        /// 是否是文件（文件夹=false,文件=true）
        /// </summary> 
        public bool IsFile { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get;set;}
         
        /// <summary>
        /// 标记是否需要删除
        /// </summary>
        [JsonIgnore]
        public bool NeedDelete { get;set;}

        /// <summary>
        /// 是否有XML内容
        /// </summary>
        [JsonIgnore]
        public bool HasXml { get; set; }

        /// <summary>
        /// 目录子节点
        /// </summary>
        public virtual List<CatalogueFileTreeDto> Catalogues { get; set; } = new List<CatalogueFileTreeDto>();

    }

}
