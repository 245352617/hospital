using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.Basic
{
    /// <summary>
    /// 医嘱项目（药品、处置、检查、检验）基本属性
    /// 因为医嘱套餐的药品、处置、检查、检验等基本信息要与字典的信息同步，相关字典修改的时候也需要同步修改，并且体现在套餐的修改上
    /// 所以对于需要根据字典修改变化的属性单独提取到该类型中，并且这些属性在新增、编辑套餐时是无法修改的（我们称之为基本属性）
    /// ***** 对于附加属性的说明 ******
    /// RecipeExamProp/RecipeMedicineProp/RecipeLabProp/RecipeTreatProp 均为 RecipeProject 的附加属性
    /// RecipeProject + 子表 才是一个完整的信息，所以4个子表与 RecipeProject 使用相同的主键（实际上如果你嫌麻烦，你完全可以把4个子表的字段剪出来放在同一个表里）（我个人喜欢把字段分表）
    /// ***************************
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    [Comment("医嘱项目")]
    public class RecipeProject : Entity<Guid>
    {
        public RecipeProject()
        {

        }

        public RecipeProject(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
            if (this.MedicineProp != null) this.MedicineProp.SetId(id);
            if (this.ExamProp != null) this.ExamProp.SetId(id);
            if (this.LabProp != null) this.LabProp.SetId(id);
            if (this.TreatProp != null) this.TreatProp.SetId(id);
        }

        #region Properties

        /// <summary>
        /// 类别编码
        /// </summary>
        /// (Medicine-药品，Examine-检查，Laboratory-检验，CZ-处置，HL-护理，SW-膳食，MZ-麻醉，SS-手术，HZ-会诊，HC-耗材，QT-其他，ZT-嘱托)
        [Required]
        [StringLength(50)]
        [Comment("类别编码")]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 类别名称
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("类别名称")]
        public string CategoryName { get; set; }

        /// <summary>
        /// 源ID
        /// </summary>
        [Comment("源ID")]
        public virtual int SourceId { get; set; }

        /// <summary>
        /// 编码
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("编码")]
        public virtual string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("名称")]
        public virtual string Name { get; set; }

        /// <summary>
        /// 学名
        /// </summary>
        [StringLength(200)]
        [Comment("学名")]
        public virtual string ScientificName { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [StringLength(200)]
        [Comment("规格")]
        public virtual string Specification { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        [StringLength(20)]
        [Comment("排序号")]
        public int Sort { get; set; }

        /// <summary>
        /// 类别拼音码
        /// </summary>
        [StringLength(50)]
        [Comment("类别拼音码")]
        public string CategoryPyCode { get; set; }

        /// <summary>
        /// 类别五笔码
        /// </summary>
        [StringLength(50)]
        [Comment("类别五笔码")]
        public virtual string CategoryWbCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [StringLength(100)]
        [Comment("拼音码")]
        public virtual string PyCode { get; set; }

        /// <summary>
        /// 五笔码
        /// </summary>
        [StringLength(100)]
        [Comment("五笔码")]
        public virtual string WbCode { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [StringLength(200)]
        [Comment("别名")]
        public virtual string Alias { get; set; }

        /// <summary>
        /// 别名拼音码
        /// </summary>
        [StringLength(50)]
        [Comment("别名拼音码")]
        public virtual string AliasPyCode { get; set; }

        /// <summary>
        /// 别名五笔码
        /// </summary>
        [StringLength(50)]
        [Comment("别名五笔码")]
        public virtual string AliasWbCode { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        [StringLength(50)]
        [Comment("执行科室编码")]
        public virtual string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室
        /// </summary>
        [StringLength(100)]
        [Comment("执行科室")]
        public virtual string ExecDeptName { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [StringLength(50)]
        [Comment("单位")]
        public virtual string Unit { get; set; }

        /// <summary>
        /// 单位价格
        /// </summary>
        [Comment("单位价格")]
        public virtual decimal Price { get; set; }

        /// <summary>
        /// 其他价格
        /// </summary>
        [Comment("其他价格")]
        public virtual decimal OtherPrice { get; set; }

        /// <summary>
        /// 加收标志	
        /// </summary>
        [Comment("加收标志")]
        public bool Additional { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 备注/说明
        /// </summary>
        [StringLength(1000)]
        [Comment("备注/说明")]
        public virtual string Remarks { get; set; }

        /// <summary>
        /// 是否可用于院前急救急救
        /// </summary>
        [Comment("是否可用于院前急救急救")]
        public bool CanUseInFirstAid { get; set; }


        /// <summary>
        /// 收费分类代码
        /// </summary>
        [StringLength(50)]
        [Comment("收费分类代码")]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费分类名称
        /// </summary>
        [StringLength(50)]
        [Comment("收费分类名称")]
        public string ChargeName { get; set; }

        ///// <summary>
        ///// 医嘱类型编码
        ///// </summary>
        //[Comment("医嘱类型编码")]
        //[Required, StringLength(20)]
        //public string PrescribeTypeCode { get; set; }

        ///// <summary>
        ///// 医嘱类型：临嘱、长嘱、出院带药等
        ///// </summary>
        //[Comment("医嘱类型：临嘱、长嘱、出院带药等")]
        //[Required, StringLength(20)]
        //public string PrescribeTypeName { get; set; }

        #endregion

        /// <summary>
        /// 药品属性
        /// </summary>
        public virtual RecipeMedicineProp MedicineProp { get; set; }

        /// <summary>
        /// 处置属性
        /// </summary>
        public virtual RecipeTreatProp TreatProp { get; set; }

        /// <summary>
        /// 检查属性
        /// </summary>
        public virtual RecipeExamProp ExamProp { get; set; }

        /// <summary>
        /// 检验属性
        /// </summary>
        public virtual RecipeLabProp LabProp { get; set; }
    }
}
