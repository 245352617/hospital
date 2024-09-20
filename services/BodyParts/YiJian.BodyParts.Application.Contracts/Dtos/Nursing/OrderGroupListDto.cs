using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 医嘱分组列表
    /// </summary>
    public class OrderGroupListDto
    {
        /// <summary>
        /// 途径分组
        /// </summary>
        /// <example></example>
        public string NursingType { get; set; }

        public List<OrderExcuteListDto> OrderExcuteDtos { get; set; }
    }
}
