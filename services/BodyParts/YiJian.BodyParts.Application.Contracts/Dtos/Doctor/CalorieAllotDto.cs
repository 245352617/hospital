using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 热卡分配
    /// </summary>
    public class CalorieAllotDto
    {
        /// <summary>
        /// 热卡（kcal）
        /// </summary>
        public decimal? Calorie { get; set; }

        /// <summary>
        /// 蛋白质N（g）
        /// </summary>
        public decimal? Protein { get; set; }

        /// <summary>
        /// 葡萄糖G（kcal）
        /// </summary>
        public decimal? Glucose { get; set; }

        /// <summary>
        /// 脂肪乳L（kcal）
        /// </summary>
        public decimal? FatEmulsion { get; set; }

        /// <summary>
        /// 热氮比
        /// </summary>
        public string HotNitrogen { get; set; }

        /// <summary>
        /// 糖脂比
        /// </summary>
        public string SugarFat { get; set; }
    }
}
