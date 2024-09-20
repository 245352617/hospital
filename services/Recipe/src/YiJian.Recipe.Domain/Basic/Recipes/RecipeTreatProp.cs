using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.Basic
{
    /// <summary>
    /// 医嘱-处置属性
    /// Author: ywlin
    /// Date: 2022-05-19
    /// </summary>
    [Comment("医嘱-处置属性")]
    public class RecipeTreatProp : Entity<Guid>
    {
        public RecipeTreatProp()
        {

        }

        public RecipeTreatProp(Guid id, string projectType, string projectName)
        {
            this.Id = id;
            this.ProjectType = projectType;
            this.ProjectName = projectName;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        /// <summary>
        /// 默认频次代码
        /// </summary>
        [StringLength(50)]
        [Comment("默认频次代码")]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary>
        [StringLength(50)]
        [Comment("收费大类代码")]
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary>
        [StringLength(50)]
        [Comment("收费小类代码")]
        public string FeeTypeSubCode { get; set; }

        /// <summary>
        /// 项目归类 --龙岗字典所需
        /// </summary>
        [Comment("项目归类--龙岗字典所需")]
        [StringLength(200)]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 诊疗处置类别代码
        /// </summary>
        [StringLength(20)]
        [Comment("诊疗处置类别代码")]
        public string ProjectType { get; set; }

        /// <summary>
        /// 诊疗处置类别名称
        /// </summary>
        [StringLength(50)]
        [Comment("诊疗处置类别名称")]
        public string ProjectName { get; set; }

        /// <summary>
        /// 医嘱主要内容
        /// </summary>
        public virtual RecipeProject RecipeProject { get; set; }
    }
}
