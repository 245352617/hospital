using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.Hospitals.Enums;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 快速开嘱的医嘱信息
    /// </summary>
    public class QuickStartSampleAdviceDto : EntityDto<Guid?>
    {

        /// <summary>
        /// 药品id （新，重要）
        /// </summary>  
        [Required(ErrorMessage = "MedicineId必填")]
        public int MedicineId { get; set; }

        /// <summary>
        /// 数量
        /// </summary>  
        public decimal Qty { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        public string DosageForm { get; set; }

        /// <summary>
        /// 剂量
        /// </summary> 
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Required(ErrorMessage = "剂量单位必填")]
        [StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 基本单位
        /// </summary>
        [Required(ErrorMessage = "单位必填")]
        [StringLength(20)]
        public string Unit { get; set; }

        /// <summary>
        /// 大包装价格
        /// </summary>
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量(大包装系数)
        /// </summary>
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 大包装单位
        /// </summary>
        public string BigPackUnit { get; set; }

        /// <summary>
        /// 小包装单价
        /// </summary>
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary>
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装量(小包装系数)
        /// </summary>
        public int SmallPackFactor { get; set; }

        /// <summary>
        /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
        /// </summary>
        public string HisUnit { get; set; }

        /// <summary>
        /// 默认用法编码
        /// </summary>
        [StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 默认用法名称
        /// </summary>
        [StringLength(50)]
        public string UsageName { get; set; }

        /// <summary>
        /// 默认频次编码
        /// </summary>
        [Required(ErrorMessage = "默认频次信息编码必填"), StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 默认频次名称
        /// </summary>
        [StringLength(50)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        [StringLength(50)]
        public string MedicalInsuranceCode { get; set; }

        /// <summary>
        /// 备注(医嘱说明)
        /// </summary>
        [StringLength(20)]
        public string Remark { get; set; }

        /// <summary>
        /// 皮试药
        /// </summary> 
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 快速开嘱选择的药品编码
        /// </summary>
        [Required]
        [StringLength(50)]
        public string MedicineCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [Required]
        [StringLength(250)]
        public string MedicineName { get; set; }

        #region 后添加的医嘱相关的信息

        private decimal _recieveQty;

        /// <summary>
        /// 领量(数量)
        /// </summary>
        public decimal RecieveQty
        {
            get
            {
                if (_recieveQty == 0)
                {
                    return Qty;
                }
                return _recieveQty;
            }
            set
            {
                if (value == 0)
                {
                    _recieveQty = Qty;
                }
                else
                {
                    _recieveQty = value;
                }
            }
        }

        private string _recieveUnit;

        /// <summary>
        /// 领量单位
        /// </summary> 
        [StringLength(20)]
        public string RecieveUnit
        {
            get
            {
                if (_recieveUnit.IsNullOrEmpty())
                {
                    return Unit;
                }
                return _recieveUnit;
            }
            set
            {
                if (value.IsNullOrEmpty())
                {
                    _recieveUnit = Unit;
                }
                else
                {
                    _recieveUnit = value;
                }
            }
        }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二） 
        /// </summary> 
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int LongDays { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary> 
        public int? ActualDays { get; set; }

        /// <summary>
        /// 默认规格剂量
        /// </summary> 
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary> 
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 默认规格剂量单位
        /// </summary> 
        public string DefaultDosageUnit { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary> 
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
        /// </summary> 
        [StringLength(20)]
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
        /// </summary> 
        [StringLength(500)]
        public string FrequencyExecDayTimes { get; set; }

        /// <summary>
        /// 失效期
        /// </summary> 
        public DateTime? ExpirDate { get; set; }


        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary> 
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 耗材金额
        /// </summary> 
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 用于判断关联耗材是否手动删除
        /// </summary> 
        public bool? IsBindingTreat { get; set; }

        /// <summary>
        /// 是否抢救后补：false=非抢救后补，true=抢救后补
        /// </summary> 
        public bool? IsAmendedMark { get; set; }

        /// <summary>
        /// 是否医保适应症
        /// </summary> 
        public bool? IsAdaptationDisease { get; set; }

        /// <summary>
        /// 滴速
        /// </summary> 
        [StringLength(20)]
        public string Speed { get; set; }

        #endregion

        /// <summary>
        /// 快速医嘱Id
        /// </summary>
        public Guid? QuickStartAdviceId { get; set; }

        /// <summary>
        /// 排序序号
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 是否危急药
        /// </summary>
        public bool IsCriticalPrescription { get; set; }

    }

}
