using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养计算Dto
    /// </summary>
    public class DocNutritionTotalDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理日期
        /// </summary>
        public string NurseDate { get; set; }

        /// <summary>
        /// 目标热卡分配
        /// </summary>
        public CalorieGoalDto targetDto { get; set; } = new CalorieGoalDto();

        /// <summary>
        /// 实际热卡分配
        /// </summary>
        public CalorieAllotDto actualDto { get; set; } = new CalorieAllotDto();

        /// <summary>
        /// 营养医嘱(肠内)
        /// </summary>
        public List<DocNutritionDrugDto> inDrugDtos { get; set; }

        /// <summary>
        /// 营养医嘱(肠外)
        /// </summary>
        public List<DocNutritionDrugDto> outDrugDtos { get; set; }
    }
}
