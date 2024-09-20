using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.Recipes.Preferences.Entities
{
    /// <summary>
    /// 快速开嘱的药品实体
    /// </summary>
    [Comment("快速开嘱的药品")]
    public class QuickStartMedicine : FullAuditedAggregateRoot<Guid>
    {
        private QuickStartMedicine()
        {
        }

        public QuickStartMedicine(Guid id)
        {
            Id = id;
        }


        /// <summary>
        /// 药品id （新，重要）
        /// </summary> 
        [Comment("药品id")]
        public int MedicineId { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary>
        [Comment("医嘱项目分类编码")]
        [Required, StringLength(20)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary>
        [Comment("医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)")]
        [Required, StringLength(20)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary>
        [Comment("是否补录")]
        public bool IsBackTracking { get; set; } = false;

        /// <summary>
        /// 处方号
        /// </summary>
        [Comment("处方号")]
        [StringLength(20)]
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        [Comment("开嘱时间")]
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary>
        [Comment("申请医生编码")]
        [StringLength(20)]
        public string ApplyDoctorCode { get; set; }


        /// <summary>
        /// 申请医生
        /// </summary>
        [Comment("申请医生")]
        [StringLength(50)]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        [Comment("申请科室编码")]
        [StringLength(20)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        [Comment("申请科室")]
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 管培生代码
        /// </summary>
        [Comment("管培生代码")]
        [StringLength(20)]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生名称
        /// </summary>
        [Comment("管培生名称")]
        [StringLength(50)]
        public string TraineeName { get; set; }

        /// <summary>
        /// 执行者编码
        /// </summary>
        [Comment("执行者编码")]
        [StringLength(20)]
        public string ExecutorCode { get; set; }

        /// <summary>
        /// 执行者名称
        /// </summary>
        [Comment("执行者名称")]
        [StringLength(50)]
        public string ExecutorName { get; set; }

        /// <summary>
        /// 停嘱医生编码
        /// </summary> 
        [Comment("停嘱医生编码")]
        public string StopDoctorCode { get; set; }

        /// <summary>
        /// 停嘱医生名称
        /// </summary> 
        [Comment("停嘱医生名称")]
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 停嘱时间
        /// </summary> 
        [Comment("停嘱时间")]
        public DateTime? StopDateTime { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary>
        [Comment("医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费")]
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary>
        [Comment("付费类型编码")]
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary>
        [Comment("付费类型: 0=自费,1=医保,2=其它")]
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary>
        [Comment("医嘱说明")]
        [StringLength(500)]
        public string Remarks { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary>
        [Comment("收费类型编码")]
        [StringLength(20)]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary>
        [Comment("收费类型名称")]
        [StringLength(50)]
        public string ChargeName { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("药品编码")]
        public string MedicineCode { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("药品名称")]
        public string MedicineName { get; set; }

        /// <summary>
        /// 学名
        /// </summary>
        [StringLength(200)]
        [Comment("学名")]
        public string ScientificName { get; set; }

        /// <summary>
        /// 别名
        /// </summary>
        [StringLength(200)]
        [Comment("别名")]
        public string Alias { get; set; }

        /// <summary>
        /// 别名拼音
        /// </summary>
        [StringLength(50)]
        [Comment("别名拼音")]
        public string AliasPyCode { get; set; }

        /// <summary>
        /// 别名五笔码
        /// </summary>
        [StringLength(50)]
        [Comment("别名五笔码")]
        public string AliasWbCode { get; set; }

        /// <summary>
        /// 拼音码
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("拼音码")]
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("五笔")]
        public string WbCode { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("药物属性：西药、中药、西药制剂、中药制剂")]
        public string MedicineProperty { get; set; }

        #region 剂量

        /// <summary>
        /// 默认剂量
        /// </summary>
        [StringLength(20)]
        [Comment("默认剂量")]
        public double DefaultDosage { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        [Comment("剂量")]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("剂量单位")]
        public string DosageUnit { get; set; }

        #endregion 剂量

        #region 基本(单位)价格

        /// <summary>
        /// 基本单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("基本单位")]
        public string Unit { get; set; }

        /// <summary>
        /// 基本单位价格
        /// </summary>
        [Comment("基本单位价格")]
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Comment("数量")]
        public decimal Qty { get; set; }

        #endregion 基本(单位)价格

        #region 大包装价格

        /// <summary>
        /// 大包装价格
        /// </summary>
        [Comment("大包装价格")]
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量(大包装系数)
        /// </summary>
        [Required]
        [Comment("大包装量(大包装系数)")]
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 大包装单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("大包装单位")]
        public string BigPackUnit { get; set; }

        #endregion 大包装价格

        #region 小包装价格

        /// <summary>
        /// 小包装单价
        /// </summary>
        [Comment("小包装单价")]
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("小包装单位")]
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装量(小包装系数)
        /// </summary>
        [Comment("小包装量(小包装系数)")]
        public int SmallPackFactor { get; set; }

        #endregion 小包装价格

        /// <summary>
        /// 儿童价格
        /// </summary>
        [Comment("儿童价格")]
        public decimal? ChildrenPrice { get; set; }

        /// <summary>
        /// 批发价格
        /// </summary>
        [Comment("批发价格")]
        public decimal? FixPrice { get; set; }

        /// <summary>
        /// 零售价格
        /// </summary>
        [Comment("零售价格")]
        public decimal? RetPrice { get; set; }

        #region 医保

        /// <summary>
        /// 医保目录编码
        /// </summary>
        [Comment("医保目录编码")]
        [StringLength(20)]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary>
        [Comment("医保目录:0=自费,1=甲类,2=乙类,3=其它")]
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 医保支付比例
        /// </summary>
        [Comment("医保支付比例")]
        public int? InsurancePayRate { get; set; }

        #endregion 医保

        #region 厂家

        /// <summary>
        /// 厂家名称
        /// </summary>
        [StringLength(100)]
        [Comment("厂家名称")]
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary>
        [StringLength(50)]
        [Comment("厂家代码")]
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [StringLength(20)]
        [Comment("批次号")]
        public string BatchNo { get; set; }

        /// <summary>
        /// 失效期
        /// </summary>
        [StringLength(20)]
        [Comment("失效期")]
        public DateTime? ExpireDate { get; set; }

        #endregion 厂家

        #region 重量

        /// <summary>
        /// 重量
        /// </summary>
        [Comment("重量")]
        public double? Weight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary>
        [StringLength(20)]
        [Comment("重量单位")]
        public string WeightUnit { get; set; }

        #endregion 重量

        #region 体积

        /// <summary>
        /// 体积
        /// </summary>
        [Comment("体积")]
        public double? Volume { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary>
        [StringLength(20)]
        [Comment("体积单位")]
        public string VolumeUnit { get; set; }

        #endregion 体积

        /// <summary>
        /// 备注(医嘱说明)
        /// </summary>
        [StringLength(20)]
        [Comment("备注")]
        public string Remark { get; set; }

        /// <summary>
        /// 皮试药
        /// </summary>
        [Comment("皮试药")]
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 复方药
        /// </summary>
        [Comment("复方药")]
        public bool? IsCompound { get; set; }

        /// <summary>
        /// 麻醉药
        /// </summary>
        [Comment("麻醉药")]
        public bool? IsDrunk { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary>
        [Comment("精神药  0非精神药,1一类精神药,2二类精神药")]
        public int? ToxicLevel { get; set; }

        /// <summary>
        /// 高危药
        /// </summary>
        [Comment("高危药")]
        public bool? IsHighRisk { get; set; }

        /// <summary>
        /// 冷藏药
        /// </summary>
        [Comment("冷藏药")]
        public bool? IsRefrigerated { get; set; }

        /// <summary>
        /// 肿瘤药
        /// </summary>
        [Comment("肿瘤药")]
        public bool? IsTumour { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary>
        [Comment("抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级")]
        public int? AntibioticLevel { get; set; }

        /// <summary>
        /// 贵重药
        /// </summary>
        [Comment("贵重药")]
        public bool? IsPrecious { get; set; }

        /// <summary>
        /// 胰岛素
        /// </summary>
        [Comment("胰岛素")]
        public bool? IsInsulin { get; set; }

        /// <summary>
        /// 兴奋剂
        /// </summary>
        [Comment("兴奋剂")]
        public bool? IsAnaleptic { get; set; }

        /// <summary>
        /// 试敏药
        /// </summary>
        [Comment("试敏药")]
        public bool? IsAllergyTest { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary>
        [Comment("限制性用药标识")]
        public bool? IsLimited { get; set; }

        /// <summary>
        /// 限制性用药描述
        /// </summary>
        [StringLength(1024)]
        [Comment("限制性用药描述")]
        public string LimitedNote { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("规格")]
        public string Specification { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("剂型")]
        public string DosageForm { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        [StringLength(20)]
        [Comment("执行科室编码")]
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        [StringLength(50)]
        [Comment("执行科室名称")]
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 默认用法编码
        /// </summary>
        [StringLength(20)]
        [Comment("默认用法编码")]
        public string UsageCode { get; set; }

        /// <summary>
        /// 默认用法名称
        /// </summary>
        [StringLength(50)]
        [Comment("默认用法名称")]
        public string UsageName { get; set; }

        /// <summary>
        /// 默认频次编码
        /// </summary>
        [StringLength(20)]
        [Comment("默认频次编码")]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 默认频次名称
        /// </summary>
        [StringLength(50)]
        [Comment("默认频次名称")]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 每日次(HIS的配置频次信息)
        /// </summary>
        [Required(ErrorMessage = "HIS频次编码是必填项"), StringLength(20)]
        [Comment("每日次(HIS的配置频次信息)")]
        public string DailyFrequency { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        [StringLength(50)]
        [Comment("医保编码")]
        public string MedicalInsuranceCode { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        [Comment("是否启用")]
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 急救药
        /// </summary>
        [Comment("急救药")]
        public bool? IsFirstAid { get; set; }

        #region 药房

        /// <summary>
        /// 药房代码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("药房代码")]
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary>
        [Required]
        [StringLength(50)]
        [Comment("药房")]
        public string PharmacyName { get; set; }

        #endregion 药房

        #region 权限

        /// <summary>
        /// 抗生素权限
        /// </summary>
        [Comment("抗生素权限")]
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        [Comment("处方权")]
        public int PrescriptionPermission { get; set; }

        #endregion 权限

        /// <summary>
        /// 库存
        /// </summary>
        [Comment("库存")]
        public int Stock { get; set; }

        /// <summary>
        /// 基药标准  N普通,Y国基,P省基,C市基
        /// </summary>
        [StringLength(20)]
        [Comment("基药标准  N普通,Y国基,P省基,C市基")]
        public string BaseFlag { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary>
        [Comment("门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分")]
        public EMedicineUnPack Unpack { get; set; }


        #region 医嘱相关的默认值属性

        /// <summary>
        /// 领量(数量)
        /// </summary>
        [Comment("领量(数量)")]
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary>
        [Comment("领量单位")]
        [StringLength(20)]
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二） 
        /// </summary>
        [Comment("药理等级")]
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary>
        [Comment("开药天数")]
        public int LongDays { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary>
        [Comment("实际天数")]
        public int? ActualDays { get; set; }

        /// <summary>
        /// 默认规格剂量
        /// </summary>
        [Comment("默认规格剂量")]
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary>
        [Comment("每次用量")]
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 默认规格剂量单位
        /// </summary>
        [Comment("默认规格剂量单位")]
        public string DefaultDosageUnit { get; set; }

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
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary>
        [Comment("皮试结果:false=阴性 ture=阳性")]
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        [Comment("皮试选择结果,默认空什么都没选择，0=否，1=是，2=续用")]
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 耗材金额
        /// </summary>
        [Comment("耗材金额")]
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 用于判断关联耗材是否手动删除
        /// </summary>
        [Comment("用于判断关联耗材是否手动删除")]
        public bool? IsBindingTreat { get; set; }

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
        /// 滴速
        /// </summary>
        [Comment("滴速")]
        [StringLength(20)]
        public string Speed { get; set; }

        #endregion

        /// <summary>
        /// 收费类型
        /// </summary> 
        [Comment("收费类型")]
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 快速开嘱医嘱信息Id
        /// </summary>
        [Comment("快速开嘱医嘱信息Id")]
        public Guid QuickStartAdviceId { get; set; }

        /// <summary>
        /// 是否危急药
        /// </summary>
        [Comment("是否危急药")]
        public bool IsCriticalPrescription { get; set; }

        /// <summary>
        /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
        /// </summary>
        [Comment("剂量单位-视图里面的那个Unit单位")]
        [StringLength(20)]
        public string HisUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary>
        [Comment("每次剂量-标准单位")]
        [StringLength(20)]
        public string HisDosageUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary>
        [Comment("每次剂量-标准单位的数量")]
        public decimal HisDosageQty { get; set; }

        /// <summary>
        /// 提交的一次剂量数量，急诊换算后提交给HIS的一次剂量
        /// </summary>
        [Comment("急诊换算之后-每次剂量-标准单位的数量")]
        public decimal CommitHisDosageQty { get; private set; }

        /// <summary>
        /// 更新关键字段
        /// </summary> 
        public void Update(
            int medicineId,
            string medicineCode,
            string medicineName,
            string dosageForm,
            decimal dosageQty,
            string dosageUnit,
            decimal qty,
            string unit,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            string hisUnit,
            string usageCode,
            string usageName,
            string frequencyCode,
            string frequencyName,
            string dailyFrequency,
            string medicalInsuranceCode,
            bool? isSkinTest,
            string remark,
            Guid quickStartAdviceId)
        {
            MedicineId = medicineId;
            MedicineCode = medicineCode;
            MedicineName = medicineName;
            DosageQty = dosageQty;
            DosageUnit = dosageUnit;
            DosageForm = dosageForm;
            Qty = qty;
            Unit = unit;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackFactor = smallPackFactor;
            SmallPackUnit = smallPackUnit;
            HisUnit = hisUnit;
            UsageCode = usageCode;
            UsageName = usageName;
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            DailyFrequency = dailyFrequency;
            MedicalInsuranceCode = medicalInsuranceCode;
            IsSkinTest = isSkinTest;
            Remark = remark;
            QuickStartAdviceId = quickStartAdviceId;
        }

        /// <summary>
        /// 更新医嘱相关部分内容
        /// </summary> 
        public void UpdateAdvicePart(
            decimal recieveQty,
            string recieveUnit,
            string toxicProperty,
            int longDays,
            int? actualDays,
            decimal defaultDosageQty,
            decimal? qtyPerTimes,
            string defaultDosageUnit,
            bool? skinTestResult,
            decimal? materialPrice,
            bool? isBindingTreat,
            bool? isAmendedMark,
            bool? isAdaptationDisease,
            string speed,
            ERestrictedDrugs? restrictedDrugs,
            bool isCriticalPrescription)
        {
            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;
            ToxicProperty = toxicProperty;
            LongDays = longDays;
            ActualDays = actualDays;
            DefaultDosageQty = defaultDosageQty;
            QtyPerTimes = qtyPerTimes;
            DefaultDosageUnit = defaultDosageUnit;
            SkinTestResult = skinTestResult;
            MaterialPrice = materialPrice;
            IsBindingTreat = isBindingTreat;
            IsAmendedMark = isAmendedMark;
            IsAdaptationDisease = isAdaptationDisease;
            Speed = speed;
            RestrictedDrugs = restrictedDrugs;
            IsCriticalPrescription = isCriticalPrescription;
        }

        /// <summary>
        ///  更新药品信息
        /// </summary>
        /// <param name="medicineId"></param>
        /// <param name="specification"></param>
        /// <param name="pharmacyCode"></param>
        /// <param name="pharmacyName"></param>
        /// <param name="factoryCode"></param>
        /// <param name="factoryName"></param>
        public void UpdateMedicineInfo(
          int medicineId,
          string specification,
          string pharmacyCode,
          string pharmacyName,
          string factoryCode,
          string factoryName)
        {
            MedicineId = medicineId;
            Specification = specification;
            PharmacyCode = pharmacyCode;
            PharmacyName = pharmacyName;
            FactoryCode = factoryCode;
            FactoryName = factoryName;
        }

        /// <summary>
        /// 更新频次信息
        /// </summary>
        /// <param name="frequencyCode">默认频次编码</param>
        /// <param name="frequencyName">默认频次编码名称</param>
        /// <param name="frequencyTimes">在一个周期内执行的次数</param>
        /// <param name="frequencyUnit">周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时</param>
        /// <param name="frequencyExecDayTimes">一天内的执行时间</param>
        /// <param name="dailyFrequency">HIS频次编码</param>
        public void UpdateFrequency(string frequencyCode, string frequencyName, int? frequencyTimes, string frequencyUnit, string frequencyExecDayTimes, string dailyFrequency)
        {
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            FrequencyTimes = frequencyTimes;
            FrequencyUnit = frequencyUnit;
            FrequencyExecDayTimes = frequencyExecDayTimes;
            DailyFrequency = dailyFrequency;
        }

        public void SetId(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 整个药品都更新了
        /// </summary>
        public void ChangeDrug(
            string categoryCode,
            string categoryName,
            string chargeCode,
            string chargeName,
            string scientificName,
            string alias,
            string aliasPyCode,
            string aliasWbCode,
            string pyCode,
            string wbCode,
            string medicineProperty,
            double defaultDosage,
            decimal dosageQty,
            decimal price,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            string insuranceCode,
            EInsuranceCatalog insuranceType,
            string factoryName,
            string factoryCode,
            string batchNo,
            string volumeUnit,
            string limitedNote,
            string specification,
            string dosageForm,
            string pharmacyCode,
            string pharmacyName,
            int antibioticPermission,
            int prescriptionPermission,
            string baseFlag,
            EMedicineUnPack unpack,
            decimal? childrenPrice,
            decimal? fixPrice,
            decimal? retPrice,
            int? insurancePayRate,
            double? weight,
            string weightUnit,
            double? volume,
            bool? isCompound,
            bool? isDrunk,
            int? toxicLevel,
            bool? isHighRisk,
            bool? isRefrigerated,
            bool? isTumour,
            int? antibioticLevel,
            bool? isPrecious,
            bool? isInsulin,
            bool? isAnaleptic,
            bool? isAllergyTest,
            bool? isLimited,
            bool? isFirstAid,
            string hisUnit,
            string hisDosageUnit,
            decimal hisDosageQty)
        {
            CategoryCode = categoryCode;
            CategoryName = categoryName;
            ChargeCode = chargeCode;
            ChargeName = chargeName;
            ScientificName = scientificName;
            Alias = alias;
            AliasPyCode = aliasPyCode;
            AliasWbCode = aliasWbCode;
            PyCode = pyCode;
            WbCode = wbCode;
            MedicineProperty = medicineProperty;
            DefaultDosage = defaultDosage;
            DosageQty = dosageQty;
            Price = price;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackUnit = smallPackUnit;
            SmallPackFactor = smallPackFactor;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;
            FactoryName = factoryName;
            FactoryCode = factoryCode;
            BatchNo = batchNo;
            VolumeUnit = volumeUnit;
            LimitedNote = limitedNote;
            Specification = specification;
            DosageForm = dosageForm;
            PharmacyCode = pharmacyCode;
            PharmacyName = pharmacyName;
            AntibioticPermission = antibioticPermission;
            PrescriptionPermission = prescriptionPermission;
            BaseFlag = baseFlag;
            Unpack = unpack;
            ChildrenPrice = childrenPrice;
            FixPrice = fixPrice;
            RetPrice = retPrice;
            InsurancePayRate = insurancePayRate;
            Weight = weight;
            WeightUnit = weightUnit;
            Volume = volume;
            IsCompound = isCompound;
            IsDrunk = isDrunk;
            ToxicLevel = toxicLevel;
            IsHighRisk = isHighRisk;
            IsRefrigerated = isRefrigerated;
            IsTumour = isTumour;
            AntibioticLevel = antibioticLevel;
            IsPrecious = isPrecious;
            IsInsulin = isInsulin;
            IsAnaleptic = isAnaleptic;
            IsAllergyTest = isAllergyTest;
            IsLimited = isLimited;
            IsFirstAid = isFirstAid;
            HisUnit = hisUnit;
            HisDosageUnit = hisDosageUnit;
            HisDosageQty = hisDosageQty;
        }

        /// <summary>
        /// 设置提交给HIS的一次领量
        /// </summary> 
        /// <returns></returns>
        public void SetHisDosageQty()
        {
            //急诊的一次剂量单位和HIS的一次剂量一致
            if (DosageUnit == HisDosageUnit)
            {
                CommitHisDosageQty = DosageQty;
                return;
            }

            switch (Unpack)
            {
                //Unpack = 0
                case EMedicineUnPack.RoundByMinUnitAmount:
                //Unpack = 1
                case EMedicineUnPack.RoundByPackUnitAmount:
                    {
                        if (DosageUnit == HisUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * HisDosageQty, 3);
                        }
                        else if (DosageUnit == SmallPackUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * SmallPackFactor * HisDosageQty, 3);
                        }
                        else if (DosageUnit == BigPackUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * BigPackFactor * HisDosageQty, 3);
                        }
                    }
                    break;
                //Unpack = 2
                case EMedicineUnPack.RoundByMinUnitTime:
                //Unpack = 3
                case EMedicineUnPack.RoundByPackUnitTime:
                    {
                        if (DosageUnit == HisUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * HisDosageQty, 3);
                        }
                        else if (DosageUnit == SmallPackUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * (BigPackFactor * HisDosageQty), 3);
                        }
                        else if (DosageUnit == BigPackUnit)
                        {
                            CommitHisDosageQty = Math.Round(DosageQty * (SmallPackFactor * HisDosageQty), 3);
                        }
                    }
                    break;
                default:
                    break;
            }
        }


        /// <summary>
        /// 重新计算领量
        /// </summary>
        /// <returns></returns>
        public void ComputeQty()
        {
            if (!FrequencyTimes.HasValue) return;
            var num = FrequencyTimes.Value * LongDays;

            switch (Unpack)
            {
                case EMedicineUnPack.RoundByMinUnitAmount: //0

                    if (DosageUnit.Trim() == DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling((次数*Ceiling((开的剂量/规格剂量))/小包装拆零系数)) 
                        RecieveQty = Math.Ceiling((num * Math.Ceiling(DosageQty / DefaultDosageQty)) / SmallPackFactor);
                    }
                    else if (DosageUnit.Trim() == Unit.Trim())
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量))/小包装拆零系数
                        RecieveQty = Math.Ceiling(num * Math.Ceiling(DosageQty) / SmallPackFactor);
                    }
                    else if (DosageUnit.Trim() == BigPackUnit.Trim() || DosageUnit.Trim() == SmallPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量)
                        RecieveQty = Math.Ceiling(num * DosageQty);
                    }

                    break;
                case EMedicineUnPack.RoundByPackUnitAmount: //1
                    if (DosageUnit.Trim() == DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling((次数*(开的剂量/规格剂量))/小包装拆零系数)) 
                        RecieveQty = Math.Ceiling((num * (DosageQty / DefaultDosageQty) / SmallPackFactor));
                    }
                    else if (DosageUnit.Trim() == Unit.Trim())
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*开的剂量)/小包装拆零系数
                        RecieveQty = Math.Ceiling((num * DosageQty) / SmallPackFactor);
                    }
                    else if (DosageUnit.Trim() == BigPackUnit.Trim() || DosageUnit.Trim() == SmallPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量) 
                        RecieveQty = Math.Ceiling(num * DosageQty);
                    }

                    break;
                case EMedicineUnPack.RoundByMinUnitTime: //2
                    if (DosageUnit.Trim() == DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量= Ceiling(次数*Ceiling(开的剂量/规格剂量)/大包装拆零系数)  
                        RecieveQty = Math.Ceiling(num * Math.Ceiling((Math.Ceiling(DosageQty / DefaultDosageQty)) / BigPackFactor));
                    }
                    else if (DosageUnit.Trim() == Unit)
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量)/大包装拆零系数)
                        RecieveQty = Math.Ceiling(num * Math.Ceiling((Math.Ceiling(DosageQty) / BigPackFactor)));
                    }
                    else if (DosageUnit.Trim() == SmallPackUnit.Trim())
                    {
                        // 剂量单位===小包装单位
                        // 数量=次数*Ceiling(开的剂量)
                        RecieveQty = Math.Ceiling(DosageQty) * num;
                    }
                    else if (DosageUnit.Trim() == BigPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*开的剂量*小包装拆零系数/大包装拆零系数) 
                        RecieveQty = Math.Ceiling(((SmallPackFactor / BigPackFactor) * DosageQty) * num);
                    }

                    break;
                case EMedicineUnPack.RoundByPackUnitTime: //3
                    if (DosageUnit.Trim() == DefaultDosageUnit.Trim())
                    {
                        // 剂量单位===默认剂量单位
                        // 数量=Ceiling(次数*开的剂量/规格剂量/大包装拆零系数)
                        RecieveQty = Math.Ceiling(num * ((DosageQty / DefaultDosageQty) / BigPackFactor));
                    }
                    else if (DosageUnit == Unit)
                    {
                        // 剂量单位===基本单位
                        // 数量=Ceiling(次数*开的剂量/大包装拆零系数)
                        RecieveQty = Math.Ceiling((DosageQty / BigPackFactor) * num);
                    }
                    else if (DosageUnit.Trim() == SmallPackUnit.Trim())
                    {
                        // 剂量单位===小包装单位
                        // 数量=Ceiling(次数*开的剂量)
                        RecieveQty = Math.Ceiling(DosageQty * num);
                    }
                    else if (DosageUnit.Trim() == BigPackUnit.Trim())
                    {
                        // 剂量单位===大包装单位
                        // 数量=Ceiling(次数*Ceiling(开的剂量*小包装拆零系数/大包装拆零系数))
                        RecieveQty = Math.Ceiling(Math.Ceiling((SmallPackFactor / BigPackFactor) * DosageQty) * num);
                    }
                    break;
            }
        }

    }
}
