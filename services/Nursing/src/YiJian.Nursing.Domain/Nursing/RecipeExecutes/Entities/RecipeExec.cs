using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Nursing.Recipes.Entities;

namespace YiJian.Nursing.RecipeExecutes.Entities
{
    /// <summary>
    /// 拆分记录表(执行单)
    /// </summary>
    [Comment("拆分记录表")]
    public partial class RecipeExec : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 关联医嘱表编号
        /// </summary>
        [Comment("关联医嘱表编号")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        [Comment("医嘱号")]
        [Required, StringLength(20)]
        public string RecipeNo { get; set; }

        /// <summary>
        /// 病人标识
        /// </summary>
        [Comment("病人标识")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary>
        [Comment("系统标识: 0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        [StringLength(20)]
        [Comment("用法编码")]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        [StringLength(50)]
        [Comment("用法名称")]
        public string UsageName { get; set; }

        /// <summary>
        /// 拆分时间
        /// </summary>
        [Comment("拆分时间")]
        public DateTime ConversionTime { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        [Comment("计划执行时间")]
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>
        [Comment("总剂量")]
        public decimal TotalDosage { get; set; }

        /// <summary>
        /// 备用量
        /// </summary>
        [Comment("备用量")]
        public decimal ReserveDosage { get; set; }

        /// <summary>
        /// 总执行量
        /// </summary>
        [Comment("总执行量")]
        public decimal TotalExecDosage { get; set; }

        /// <summary>
        /// 总余量
        /// </summary>
        [Comment("总余量")]
        public decimal TotalRemainDosage { get; set; }

        /// <summary>
        /// 废弃量
        /// </summary>
        [Comment("废弃量")]
        public decimal TotalDiscardDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        [StringLength(30)]
        [Comment("计量单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 是否废弃
        /// </summary>
        [Comment("是否废弃")]
        public bool IsDiscard { get; set; }

        /// <summary>
        /// 护士站执行单状态
        /// </summary>
        [Comment("护士站执行单状态")]
        public ExecuteStatusEnum ExecuteStatus { get; set; }

        /// <summary>
        /// 核对护士
        /// </summary>
        [StringLength(20)]
        [Comment("核对护士")]
        public string CheckNurseCode { get; set; }

        /// <summary>
        /// 核对护士名称
        /// </summary>
        [StringLength(50)]
        [Comment("核对护士名称")]
        public string CheckNurseName { get; set; }

        /// <summary>
        /// 核对时间
        /// </summary>
        [Comment("核对时间")]
        public DateTime? CheckTime { get; set; }

        /// <summary>
        /// 二次核对护士
        /// </summary>
        [StringLength(20)]
        [Comment("二次核对护士")]
        public string TwoCheckNurseCode { get; set; }

        /// <summary>
        /// 二次核对护士名称
        /// </summary>
        [StringLength(50)]
        [Comment("二次核对护士名称")]
        public string TwoCheckNurseName { get; set; }

        /// <summary>
        /// 二次核对时间
        /// </summary>
        [Comment("二次核对时间")]
        public DateTime? TwoCheckTime { get; set; }

        /// <summary>
        /// 执行护士
        /// </summary>
        [StringLength(20)]
        [Comment("执行护士")]
        public string ExecuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>
        [StringLength(50)]
        [Comment("执行护士名称")]
        public string ExecuteNurseName { get; set; }

        /// <summary>
        /// 护士执行时间
        /// </summary>
        [Comment("护士执行时间")]
        public DateTime? ExecuteNurseTime { get; set; }

        /// <summary>
        /// 执行方式PC/PDA
        /// </summary>
        [StringLength(30)]
        [Comment("执行方式PC/PDA")]
        public string ExecuteType { get; set; }

        /// <summary>
        /// 是否已经打印瓶贴
        /// </summary>
        [Comment("是否已经打印瓶贴")]
        public bool IsPrint { get; set; }

        /// <summary>
        /// 构造方法
        /// </summary>
        protected RecipeExec()
        {

        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        public RecipeExec(Guid id) : base(id)
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipe"></param>
        /// <param name="execTime"></param>
        protected RecipeExec(Guid id, Recipe recipe, DateTime execTime) : base(id)
        {
            RecipeId = recipe.Id;
            RecipeNo = recipe.RecipeNo;
            PIID = recipe.PIID;
            PlatformType = recipe.PlatformType;
            ConversionTime = DateTime.Now;
            PlanExcuteTime = execTime;
            if (recipe.PayStatus == EPayStatus.HavePaid)
            {
                ExecuteStatus = ExecuteStatusEnum.UnCheck;
            }
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="id"></param>
        /// <param name="recipe"></param>
        /// <param name="prescribe"></param>
        /// <param name="execTime"></param>
        protected RecipeExec(Guid id, Recipe recipe, Prescribe prescribe, DateTime execTime)
            : this(id, recipe, execTime)
        {
            UsageCode = prescribe.UsageCode;
            UsageName = prescribe.UsageName;
            TotalDosage = prescribe.DosageQty;
            ReserveDosage = TotalDosage;
            Unit = prescribe.DosageUnit;
            if (recipe.PayStatus == EPayStatus.HavePaid)
            {
                ExecuteStatus = ExecuteStatusEnum.UnCheck;
            }
        }

        /// <summary>
        /// 基于药物类医嘱信息创建医嘱执行信息
        /// </summary>
        /// <param name="id">医嘱执行编号</param>
        /// <param name="recipe">医嘱单</param>
        /// <param name="prescribe">药物项</param>
        /// <param name="execTime">执行时间</param>
        /// <returns></returns>
        public static RecipeExec CreatePrescribe(Guid id, Recipe recipe, Prescribe prescribe, DateTime execTime)
        {
            var recipeExec = new RecipeExec(id, recipe, prescribe, execTime);
            return recipeExec;
        }

        /// <summary>
        /// 基于非药物类医嘱信息创建医嘱执行信息
        /// </summary>
        /// <param name="id">医嘱执行编号</param>
        /// <param name="recipe">医嘱单</param>
        /// <param name="execTime">执行时间</param>
        /// <returns></returns>
        public static RecipeExec Create(Guid id, Recipe recipe, DateTime execTime)
        {
            var recipeExec = new RecipeExec(id, recipe, execTime);
            return recipeExec;
        }
    }
}
