using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养评估列表
    /// </summary>
    public class NutritionAssessList
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 项目列表
        /// </summary>
        public List<NutritionAssessItemDto> paraItemDtoList { get; set; }
    }

    public class NutritionAssessItemDto
    {
        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 参数编号
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        /// <example></example>
        public string ParaName { get; set; }

        /// <summary>
        /// 项目值列表
        /// </summary>
        public List<NutritionAssessDetailDto> nursingDto { get; set; } = new List<NutritionAssessDetailDto>();
    }
}
