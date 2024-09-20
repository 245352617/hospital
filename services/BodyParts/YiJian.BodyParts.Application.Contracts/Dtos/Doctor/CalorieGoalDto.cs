using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养分配目标
    /// </summary>
    public class CalorieGoalDto : CalorieAllotDto
    {
        /// <summary>
        /// 身高（cm)
        /// </summary>
        public decimal? Height { get; set; }

        /// <summary>
        /// 体重（Kg)
        /// </summary>
        public decimal? Weight { get; set; }

        /// <summary>
        /// 理想体重（Kg)
        /// </summary>
        public decimal? TargetWeight { get; set; }

        /// <summary>
        /// 实际BMI（Kg/M2)
        /// </summary>
        public decimal? Bmi { get; set; }

        /// <summary>
        /// 热卡目标
        /// </summary>
        public decimal? CalorieGoal { get; set; }
    }
}
