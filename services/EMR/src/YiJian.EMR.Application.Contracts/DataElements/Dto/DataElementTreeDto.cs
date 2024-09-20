using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.DataElements.Dto
{
    /// <summary>
    /// 数据元的树
    /// </summary>
    public class DataElementTreeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 名称标题
        /// </summary> 
        [StringLength(50)]
        public string Title { get; set; }

        /// <summary>
        /// 是否是元素
        /// </summary> 
        public bool IsElement { get; set; }

        /// <summary>
        /// 父级级联Id
        /// </summary> 
        public Guid Parent { get; set; }

        /// <summary>
        /// 层级，默认=0
        /// </summary> 
        public int Lv { get; set; }

        /// <summary>
        /// 子节点
        /// </summary>
        public List<DataElementTreeDto> Children { get; set; } = new List<DataElementTreeDto>();

    }
}
