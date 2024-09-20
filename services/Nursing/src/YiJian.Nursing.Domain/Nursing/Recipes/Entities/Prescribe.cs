using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.Nursing.RecipeExecutes;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 药物项
    /// </summary>
    public class Prescribe : FullAuditedAggregateRoot<Guid>
    {
        #region 属性
        /// <summary>
        /// 医嘱主表ID
        /// </summary>
        [Comment("医嘱主表ID")]
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 是否自备药：false=非自备药,true=自备药
        /// </summary>
        [Comment("是否自备药：false=非自备药,true=自备药")]
        public bool IsOutDrug { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary>
        [Comment("药物属性：西药、中药、西药制剂、中药制剂")]
        [Required, StringLength(20)]
        public string MedicineProperty { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二），json存储
        /// </summary>
        [Comment("药理等级")]
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary>
        [Comment("用法编码")]
        [Required, StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary>
        [Comment("用法名称")]
        [Required, StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 滴速
        /// </summary>
        [Comment("滴速")]
        [StringLength(20)]
        public string Speed { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>
        [Comment("开药天数")]
        public int LongDays { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>
        [Comment("每次剂量")]
        [Required]
        [Column(TypeName = "decimal(18,4)")]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary>
        [Comment("每次用量")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Comment("剂量单位")]
        [Required, StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        [Comment("包装规格")]
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 频次码
        /// </summary>
        [Comment("频次码")]
        [Required, StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary>
        [Comment("频次")]
        [StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary>
        [Comment("在一个周期内执行的次数")]
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
        /// </summary>
        [Comment("周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时")]
        [StringLength(20)]
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
        /// </summary>
        [Comment("一天内的执行时间")]
        [StringLength(500)]
        public string FrequencyExecDayTimes { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary>
        [Comment("药房编码")]
        [StringLength(20)]
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary>
        [Comment("药房名称")]
        [StringLength(50)]
        public string PharmacyName { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary>
        [Comment("是否皮试 false=不需要皮试 true=需要皮试")]
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 耗材金额 暂时没发现有什么用 做个标记
        /// </summary>
        [Comment("耗材金额")]
        [Column(TypeName = "decimal(18,4)")]
        [Obsolete]
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 是否抢救后补：false=非抢救后补，true=抢救后补
        /// </summary>
        [Comment("是否抢救后补：false=非抢救后补，true=抢救后补")]
        public bool? IsAmendedMark { get; set; }

        /// <summary>
        /// 是否医保适应症
        /// </summary>
        [Comment("是否医保适应症")]
        public bool? IsAdaptationDisease { get; set; }

        /// <summary>
        /// 是否是急救药
        /// </summary> 
        [Comment("是否是急救药")]
        public bool? IsFirstAid { get; set; }

        #endregion  属性

        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary>
        [Comment("皮试结果:false=阴性 ture=阳性")]
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 皮试选择结果 0和2不用做皮试
        /// </summary>
        [Comment("皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用")]
        public int? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary>
        [Comment("实际天数")]
        public int? ActualDays { get; set; }

        /// <summary>
        /// 是否需要皮试 需要两个字段组合判断
        /// </summary>
        /// <returns></returns>
        public bool NeedSkinTest()
        {
            if (IsSkinTest.HasValue && IsSkinTest.Value == true)
            {
                if (SkinTestSignChoseResult.HasValue && SkinTestSignChoseResult.Value == 1)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 更新药品频次信息
        /// </summary>
        /// <param name="frequency"></param>
        public void UpdateFrequncy(Frequency frequency)
        {
            FrequencyCode = frequency.FrequencyCode;
            FrequencyName = frequency.FrequencyName;
            FrequencyUnit = frequency.Unit;
            FrequencyTimes = frequency.Times;
            FrequencyExecDayTimes = frequency.PreparedDayTime;
        }
    }
}
