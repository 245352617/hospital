using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养评估明细Dto
    /// </summary>
    public class NutritionAssessDetailDto : NutritionAssessDateDto
    {
        /// <summary>
        /// 参数代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数值
        /// </summary>
        public string ParaValue { get; set; }

        /// <summary>
        /// 参数值文本
        /// </summary>
        public string ParaValueString { get; set; }
    }
}
