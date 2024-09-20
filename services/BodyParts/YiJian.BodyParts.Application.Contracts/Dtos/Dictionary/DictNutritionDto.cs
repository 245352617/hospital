using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养字典Dto
    /// </summary>
    public class DictNutritionDto : EntityDto<Guid>
    {
        /// <summary>
        /// 营养项目分类：医嘱1；入量2
        /// </summary>
        public NutritionItemType NutritionItemType { get; set; }

        /// <summary>
        /// 药品代码/项目代码
        /// </summary>
        public string DrugCode { get; set; }

        /// <summary>
        /// 药品名称/项目名称
        /// </summary>
        public string DrugName { get; set; }

        /// <summary>
        /// 营养字典分类
        /// </summary>
        public DictNutritionType DictNutritionType { get; set; }

        /// <summary>
        /// 容量
        /// </summary>
        public decimal? Capacity { get; set; }

        /// <summary>
        /// 热卡（kcal）
        /// </summary>
        public decimal? Calorie { get; set; }

        /// <summary>
        /// 脂肪乳（kcal）
        /// </summary>
        public decimal? FatEmulsion { get; set; }

        /// <summary>
        /// 蛋白质（g）
        /// </summary>
        public decimal? Protein { get; set; }

        /// <summary>
        /// 葡萄糖（kcal）
        /// </summary>
        public decimal? Glucose { get; set; }
    }
}
