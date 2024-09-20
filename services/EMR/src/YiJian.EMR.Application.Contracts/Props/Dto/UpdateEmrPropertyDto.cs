using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.EMR.Props.Dto
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    public class UpdateEmrPropertyDto : EntityDto<Guid>
    {
        /// <summary>
        /// 属性标签
        /// </summary> 
        [Required, StringLength(32)]
        public string Label { get; set; }
    }  
}
