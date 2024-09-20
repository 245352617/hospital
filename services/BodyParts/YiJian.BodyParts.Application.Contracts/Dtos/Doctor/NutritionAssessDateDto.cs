using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养评估与监测时间Dto
    /// </summary>
    public class NutritionAssessDateDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理日期
        /// </summary>
        public string NurseDate { get; set; }
    }
}
