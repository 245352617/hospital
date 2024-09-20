using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 病情分析视图分组Dto
    /// </summary>
    public class ConditionViewGroupDto
    {
        /// <summary>
        /// 分组Id
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 视图Id
        /// </summary>
        [Required]
        public Guid Pid { get; set; }

        /// <summary>
        /// 分组名称
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 样式:药物-->数值，出入量-->柱状图，其他-->曲线图
        /// </summary>
        [Required]
        public string Style { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
