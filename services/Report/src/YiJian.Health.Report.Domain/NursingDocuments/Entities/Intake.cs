using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 入量出量
    /// </summary>
    [Comment("入量出量")]
    public class Intake : Entity<Guid>
    {
        private Intake()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="intakeType"></param>
        /// <param name="code"></param>
        /// <param name="inputModel"></param>
        /// <param name="content"></param>
        /// <param name="quantity"></param>
        /// <param name="recipeQty"></param>
        /// <param name="unitCode"></param>
        /// <param name="unit"></param>
        /// <param name="traitsCode"></param>
        /// <param name="traits"></param>
        /// <param name="source"></param>
        /// <param name="nursingRecordId"></param>
        /// <param name="recipeExecId"></param>
        /// <param name="recipeId"></param>
        /// <param name="recipeNo"></param>
        /// <param name="color"></param>
        public Intake(
            Guid id,
            EIntakeType intakeType,
            [NotNull] string code,
            string inputModel,
            [NotNull] string content,
            string quantity,
            string recipeQty,
            [NotNull] string unitCode,
            [NotNull] string unit,
            string traitsCode,
            string traits,
            int source,
            Guid nursingRecordId,
            Guid recipeExecId,
            Guid recipeId,
            string recipeNo,
            string color)
        {
            Id = id;
            IntakeType = intakeType;
            Code = Check.NotNullOrEmpty(code, nameof(code), maxLength: 50);
            InputMode = inputModel;
            Content = Check.NotNullOrEmpty(content, nameof(content), maxLength: 500);
            Quantity = quantity;
            RecipeQty = recipeQty;
            UnitCode = Check.NotNullOrEmpty(unitCode, nameof(unitCode), maxLength: 20);
            Unit = Check.NotNullOrEmpty(unit, nameof(unit), maxLength: 20);
            Color = color;
            TraitsCode = traitsCode;
            Traits = traits;
            Source = source;
            NursingRecordId = nursingRecordId;
            RecipeExecId = recipeExecId;
            RecipeNo = recipeNo;
            RecipeId = recipeId;
        }

        /// <summary>
        /// 入量出量类型（0=入量，1=出量）
        /// </summary>
        [Comment("入量出量类型（0=入量，1=出量）")]
        [Required]
        public EIntakeType IntakeType { get; set; }

        /// <summary>
        /// 出入量的代码
        /// </summary>
        [Required, StringLength(50)]
        public string Code { get; set; }

        /// <summary>
        /// 方式
        /// </summary>
        [Comment("方式")]
        [StringLength(50)]
        public string InputMode { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [Comment("内容")]
        [Required, StringLength(500)]
        public string Content { get; set; }

        /// <summary>
        /// 量
        /// </summary>
        [Comment("量")]
        [StringLength(20)]
        public string Quantity { get; set; }

        /// <summary>
        /// 医嘱药品剂量
        /// </summary>
        [Comment("医嘱药品剂量")]
        [StringLength(20)]
        public string RecipeQty { get; set; }

        /// <summary>
        /// 单位编码
        /// </summary>
        [Comment("单位编码")]
        [Required, StringLength(20)]
        public string UnitCode { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Comment("单位")]
        [Required, StringLength(20)]
        public string Unit { get; set; }

        /// <summary>
        /// 性状编码
        /// </summary>
        [Comment("性状")]
        [StringLength(20)]
        public string TraitsCode { get; set; }

        /// <summary>
        /// 性状
        /// </summary>
        [Comment("性状")]
        [StringLength(20)]
        public string Traits { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        [Comment("颜色")]
        [StringLength(20)]
        public string Color { get; set; }

        /// <summary>
        /// 来源(0：护理单录入,1：医嘱导入)
        /// </summary>
        [Comment("来源")]
        public int Source { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingRecordId { get; set; }

        /// <summary>
        /// 执行单Id
        /// </summary>
        [Comment("执行单Id")]
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        [Comment("医嘱号")]
        public string RecipeNo { get; set; }

        /// <summary>
        /// 更新实体
        /// </summary>
        /// <param name="intakeType"></param>
        /// <param name="code"></param>
        /// <param name="inputMode"></param>
        /// <param name="content"></param>
        /// <param name="quantity"></param>
        /// <param name="unitCode"></param>
        /// <param name="unit"></param>
        /// <param name="traitsCode"></param>
        /// <param name="traits"></param>
        public void Update(
           EIntakeType intakeType,
           [NotNull] string code,
           string inputMode,
           [NotNull] string content,
           string quantity,
           [NotNull] string unitCode,
           [NotNull] string unit,
           string traitsCode,
           string traits)
        {
            IntakeType = intakeType;
            Code = Check.NotNullOrEmpty(code, nameof(code), maxLength: 50);
            InputMode = inputMode;
            Content = Check.NotNullOrEmpty(content, nameof(content), maxLength: 500);
            Quantity = quantity;
            UnitCode = Check.NotNullOrEmpty(unitCode, nameof(unitCode), maxLength: 20);
            Unit = Check.NotNullOrEmpty(unit, nameof(unit), maxLength: 20);
            TraitsCode = traitsCode;
            Traits = traits;
        }
    }
}
