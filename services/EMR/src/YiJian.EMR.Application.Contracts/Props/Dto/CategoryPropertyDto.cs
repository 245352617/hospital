using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.EMR.Props.Dto
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    public class CategoryPropertyDto
    {
        /// <summary>
        /// 属性值,自己填写(如：QJ,ZDJB ...)
        /// </summary> 
        [Required, StringLength(50)]
        public string Value { get; set; }

        /// <summary>
        /// 属性标签
        /// </summary> 
        [Required, StringLength(32)]
        public string Label { get; set; }

        /// <summary>
        /// 级联父节点Id(空属于一级),如果是一级不需要传
        /// </summary> 
        public Guid? ParentId { get; set; }
    }


}
