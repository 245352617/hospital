using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.Basic
{
    /// <summary>
    /// 医嘱-检验属性
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    [Comment("医嘱-检验属性")]
    public class RecipeLabProp : Entity<Guid>
    {
        private RecipeLabProp()
        {
        }

        public RecipeLabProp(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        #region Properties

        /// <summary>
        /// 检验目录编码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("检验目录编码")]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 目录分类名称
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("目录分类名称")]
        public string CatalogName { get; set; }

        /// <summary>
        /// 标本编码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("标本编码")]
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("标本")]
        public string SpecimenName { get; set; }

        /// <summary>
        /// 位置编码
        /// </summary>
        [StringLength(20)]
        [Comment("位置编码")]
        public string PositionCode { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(100)]
        [Comment("位置")]
        public string PositionName { get; set; }

        /// <summary>
        /// 容器编码
        /// </summary>
        [StringLength(100)]
        [Comment("容器编码")]
        public string ContainerCode { get; set; }

        /// <summary>
        /// 容器名称
        /// </summary>
        [StringLength(200)]
        [Comment("容器名称")]
        public string ContainerName { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        [StringLength(50)]
        [Comment("检查部位编码")]
        public string PartCode { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary>
        [StringLength(50)]
        [Comment("检查部位名称")]
        public string PartName { get; set; }
        /// <summary>
        /// 附加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
        /// </summary> 
        [StringLength(10)]
        [Comment("加卡片类型 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)14.新型冠状病毒RNA检测申请单13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单")]
        public string AddCard { get; set; }
        /// <summary>
        /// 指引ID 关联 ExamNote表code
        /// </summary>
        [StringLength(50)]
        [Comment("指引Id 关联 ExamNote表code")]
        public string GuideCode { get; set; }

        /// <summary>
        /// 指引名称 关联 ExamNote表code
        /// </summary>
        [StringLength(2000)]
        [Comment("指引名称 关联 ExamNote表name")]
        public string GuideName { get; set; }

        /// <summary>
        /// 指引单大类
        /// </summary>
        [Comment("指引单大类")]
        public string GuideCatelogName { get; set; }

        /// <summary>
        /// 医嘱主要内容
        /// </summary>
        public virtual RecipeProject RecipeProject { get; set; }

        /// <summary>
        /// 更新标本的字段
        /// </summary> 
        public void UpdateSpecimen(
            string specimenCode,
            string specimenName
        )
        {
            SpecimenCode = specimenCode;
            SpecimenName = specimenName;
        }

        #endregion
    }
}