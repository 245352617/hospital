using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 营养药品明细Dto
    /// </summary>
    public class DocNutritionDrugDto : EntityDto<Guid>
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
        /// 营养分类
        /// </summary>
        public DictNutritionType DictNutritionType { get; set; }

        /// <summary>
        /// 药品代码
        /// </summary>
        public string DrugCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        public string DrugName { get; set; }

        /// <summary>
        /// 容量（ml）
        /// </summary>
        public decimal? Capacity { get; set; }

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
    }
}
