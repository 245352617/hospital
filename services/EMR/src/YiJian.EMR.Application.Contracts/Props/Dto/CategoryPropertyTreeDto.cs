using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Props.Dto
{
    /// <summary>
    /// 电子病历属性树
    /// </summary>
    [JsonConverter(typeof(CategoryPropertyTreeJsonConverter))]
    public class CategoryPropertyTreeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 属性值,自己填写(如：QJ,ZDJB ...)
        /// </summary>  
        public string Value { get; set; }

        /// <summary>
        /// 属性标签
        /// </summary>  
        public string Label { get; set; }

        /// <summary>
        /// 级联父节点Id(空属于一级),如果是一级不需要传
        /// </summary> 
        public Guid? ParentId { get; set; }

        /// <summary>
        /// 树层级
        /// </summary>
        public int Lv { get; set; }

        public List<CategoryPropertyTreeDto> Children { get; set; } = new List<CategoryPropertyTreeDto>();
    }
}
