using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 颜色属性Dto
    /// </summary>
    public class ColorParaItemDto
    {
        /// <summary>
        /// 单位名称
        /// </summary>
        public string UnitName { get; set; }

        /// <summary>
        /// 数据类型
        /// </summary>
        public string ValueType { get; set; }

        /// <summary>
        /// 文本类型
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 小数位数
        /// </summary>
        public string DecimalDigits { get; set; }

        /// <summary>
        /// 最大值
        /// </summary>
        public string MaxValue { get; set; }

        /// <summary>
        /// 最小值
        /// </summary>
        public string MinValue { get; set; }
    }
}
