using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病情分析视图返回参数
    /// </summary>
    public class ConditionViewDto
    {
        public Guid? Id { get; set; }

        /// <summary>
        /// 科室编号
        /// </summary>
        [Required]
        public string DeptCode { get; set; }

        /// <summary>
        /// 视图名称
        /// </summary>
        [Required] 
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
