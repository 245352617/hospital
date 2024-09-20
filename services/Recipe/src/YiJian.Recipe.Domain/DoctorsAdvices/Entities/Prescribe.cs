using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.DoctorsAdvices.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.Recipes.DoctorsAdvices.Entities
{
    /// <summary>
    /// 药品
    /// </summary>
    [Comment("药品")]
    public class Prescribe : FullAuditedAggregateRoot<Guid>
    {
        /// <summary>
        /// 药方
        /// </summary>
        private Prescribe()
        {

        }

        /// <summary>
        /// 药方
        /// </summary>
        /// <param name="id"></param>
        public Prescribe(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 药品
        /// </summary> 
        public Prescribe(Guid id,
            int medicineId,
            bool isOutDrug,
            string medicineProperty,
            string toxicProperty,
            string usageCode,
            string usageName,
            string speed,
            int longDays,
            int? actualDays,
            string dosageForm,
            decimal dosageQty,
            decimal defaultDosageQty,
            decimal? qtyPerTimes,
            string dosageUnit,
            string defaultDosageUnit,
            EMedicineUnPack unpack,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            string frequencyCode,
            string frequencyName,
            string medicalInsuranceCode,
            int? frequencyTimes,
            string frequencyUnit,
            string frequencyExecDayTimes,
            string dailyFrequency,
            string pharmacyCode,
            string pharmacyName,
            string factoryName,
            string factoryCode,
            string batchNo,
            DateTime? expirDate,
            bool? isSkinTest,
            bool? skinTestResult,
            ESkinTestSignChoseResult? skinTestSignChoseResult,
            decimal? materialPrice,
            bool? isBindingTreat,
            bool? isAmendedMark,
            bool? isAdaptationDisease,
            bool? isFirstAid,
            int antibioticPermission,
            int prescriptionPermission,
            string specification,
            decimal? childrenPrice,
            decimal? fixPrice,
            decimal? retPrice,
            ERestrictedDrugs? restrictedDrugs,
            int limitType,
            Guid doctorsAdviceId,
            bool isCriticalPrescription,
            string hisUnit,
            string hisDosageUnit,
            decimal hisDosageQty)
        //,string code,
        //string name)
        {
            Id = id;
            MedicineId = medicineId;
            IsOutDrug = isOutDrug;
            MedicineProperty = medicineProperty;
            ToxicProperty = toxicProperty;
            UsageCode = usageCode;
            UsageName = usageName;
            Speed = speed;
            LongDays = longDays;
            ActualDays = actualDays;
            DosageQty = dosageQty;
            DefaultDosageQty = defaultDosageQty;
            QtyPerTimes = qtyPerTimes;
            DosageUnit = dosageUnit;
            DefaultDosageUnit = defaultDosageUnit;
            Unpack = unpack;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackUnit = smallPackUnit;
            SmallPackFactor = smallPackFactor;
            FrequencyCode = frequencyCode;
            MedicalInsuranceCode = medicalInsuranceCode;
            FrequencyName = frequencyName;
            FrequencyTimes = frequencyTimes;
            FrequencyUnit = frequencyUnit;
            FrequencyExecDayTimes = frequencyExecDayTimes;
            DailyFrequency = dailyFrequency;
            PharmacyCode = pharmacyCode;
            PharmacyName = pharmacyName;
            FactoryName = factoryName;
            FactoryCode = factoryCode;
            BatchNo = batchNo;
            ExpirDate = expirDate;
            IsSkinTest = isSkinTest;
            SkinTestResult = skinTestResult;
            SkinTestSignChoseResult = skinTestSignChoseResult;
            MaterialPrice = materialPrice;
            IsBindingTreat = isBindingTreat;
            IsAmendedMark = isAmendedMark;
            IsAdaptationDisease = isAdaptationDisease;
            IsFirstAid = isFirstAid;
            AntibioticPermission = antibioticPermission;
            PrescriptionPermission = prescriptionPermission;
            Specification = specification;
            ChildrenPrice = childrenPrice;
            FixPrice = fixPrice;
            RetPrice = retPrice;
            RestrictedDrugs = restrictedDrugs;
            LimitType = limitType;
            DoctorsAdviceId = doctorsAdviceId;
            IsCriticalPrescription = isCriticalPrescription;
            HisUnit = hisUnit;
            HisDosageUnit = hisDosageUnit;
            HisDosageQty = hisDosageQty;
            DosageForm = dosageForm;
            //Code = Code;
            //Name = Name;
        }
        ///// <summary>
        ///// 开方编码
        ///// </summary> 
        //[Required, StringLength(20)]
        //public string Code { get; set; }
        ///// <summary>
        ///// 开方名称
        ///// </summary> 
        //[Required, StringLength(200)]
        //public string Name { get; set; }
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
        /// 药理等级：如（毒、麻、精一、精二） 
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
        /// 实际天数
        /// </summary>
        [Comment("实际天数")]
        public int? ActualDays { get; set; }

        /// <summary>
        /// 剂型
        /// </summary>
        [Comment("剂型")]
        public string DosageForm { get; set; }

        /// <summary>
        /// 每次剂量(急诊的)
        /// </summary>
        [Comment("每次剂量（急诊的）")]
        [Required]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 默认规格剂量
        /// </summary>
        [Comment("默认规格剂量")]
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 默认规格剂量单位
        /// </summary> 
        [Comment("默认规格剂量单位（急诊的）")]
        [Required, StringLength(20)]
        public string DefaultDosageUnit { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary>
        [Comment("每次用量")]
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 剂量单位（急诊的剂量单位，可能单位不固定）
        /// </summary>
        [Comment("剂量单位（急诊的）")]
        [Required, StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary>
        [Comment("门诊拆分属性 0=最小单位总量取整 1=包装单位总量取整 2=最小单位每次取整 3=包装单位每次取整 4=最小单位可拆分")]
        public EMedicineUnPack Unpack { get; set; }

        /// <summary>
        /// 包装价格
        /// </summary>
        [Comment("包装价格")]
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装系数(拆零系数)
        /// </summary>
        [Comment("大包装系数(拆零系数)")]
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        [Comment("包装单位")]
        [StringLength(20)]
        public string BigPackUnit { get; set; }

        /// <summary>
        /// 小包装单价
        /// </summary>
        [Comment("小包装单价")]
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary> 
        [Comment("小包装单位")]
        [StringLength(20)]
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装系数(拆零系数)
        /// </summary>
        [Comment("小包装系数(拆零系数)")]
        public int SmallPackFactor { get; set; }

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
        [Required, StringLength(20)]
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
        /// HIS频次编码
        /// </summary>
        [Comment("HIS频次编码")]
        [Required(ErrorMessage = "HIS频次编码是必填项"), StringLength(20)]
        public string DailyFrequency { get; set; }

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
        /// 厂家
        /// </summary>
        [Comment("厂家名称")]
        [StringLength(100)]
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary>
        [Comment("厂家代码")]
        [StringLength(20)]
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [Comment("批次号")]
        [StringLength(20)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 失效期
        /// </summary>
        [Comment("失效期")]
        public DateTime? ExpirDate { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary>
        [Comment("是否皮试 false=不需要皮试 true=需要皮试")]
        public bool? IsSkinTest { get; set; }

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
        /// 是否是急救药
        /// </summary>
        [Comment("是否是急救药")]
        public bool? IsFirstAid { get; set; }

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

        /// <summary>
        /// 医嘱Id
        /// </summary>
        [Comment("医嘱Id")]
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary>
        [Comment("包装规格")]
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary>
        [Comment("限制用药标记")]
        public int LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary> 
        [Comment("收费类型")]
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

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


        /// <summary>
        /// 药品id （新，重要）
        /// </summary> 
        [Comment("药品id")]
        public int MedicineId { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary>
        [StringLength(50)]
        [Comment("医保编码")]
        public string MedicalInsuranceCode { get; set; }

        /// <summary>
        /// 医嘱信息
        /// </summary>
        [NotMapped]
        public DoctorsAdvice DoctorsAdvice { get; set; }

        /// <summary>
        /// 影子Id
        /// </summary>
        [NotMapped]
        public Guid? OldDoctorsAdviceId { get; set; }

        /// <summary>
        /// 是否危急处方  是:true ，否:false , 未处理
        /// </summary>
        [Comment("是否危急处方 1=是 ，0=否")]
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
        public decimal CommitHisDosageQty { get; set; }

        ///// <summary>
        ///// 设置剂量相关的数据
        ///// </summary>
        ///// <param name="hisUnit"></param>
        ///// <param name="hisDosageUnit"></param>
        ///// <param name="hisDosageQty"></param>
        //public void SetHisDosageInfo(string hisUnit, string hisDosageUnit, decimal hisDosageQty)
        //{
        //    HisUnit = hisUnit;
        //    HisDosageUnit = hisDosageUnit;
        //    HisDosageQty = hisDosageQty;
        //}

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
        /// 克隆的时候需要重新设置关联关系
        /// </summary>
        /// <param name="id"></param>
        /// <param name="doctorsAdviceId"></param> 
        public void ResetId([NotNull] Guid id, [NotNull] Guid doctorsAdviceId)
        {
            Id = id;
            OldDoctorsAdviceId = DoctorsAdviceId;
            DoctorsAdviceId = doctorsAdviceId;
            DoctorsAdvice = null;
        }

        public void DrugsLimit(int limitType, ERestrictedDrugs? restrictedDrugs)
        {
            LimitType = limitType;
            RestrictedDrugs = restrictedDrugs;
        }

        /// <summary>
        /// 从Medicine药品字典同步数据
        /// </summary>
        public void SyncDataByMedicine(
            string medicineProperty,
            string usageCode,
            string usageName,
            decimal dosageQty,
            string dosageUnit,
            string specification,
            EMedicineUnPack unpack,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            string frequencyCode,
            string frequencyName,
            string pharmacyCode,
            string pharmacyName,
            string factoryName,
            string factoryCode,
            string batchNo,
            DateTime? expirDate,
            bool? isSkinTest,
            ESkinTestSignChoseResult? skinTestSignChoseResult,
            bool? isFirstAid,
            int antibioticPermission,
            int prescriptionPermission,
            bool isCriticalPrescription,
            string hisUnit,
            string hisDosageUnit,
            decimal hisDosageQty
            )
        {
            MedicineProperty = medicineProperty;
            UsageCode = usageCode;
            UsageName = usageName;
            DosageQty = dosageQty;
            DosageUnit = dosageUnit;
            Specification = specification;
            Unpack = unpack;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackUnit = smallPackUnit;
            SmallPackFactor = smallPackFactor;
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            PharmacyCode = pharmacyCode;
            PharmacyName = pharmacyName;
            FactoryName = factoryName;
            FactoryCode = factoryCode;
            BatchNo = batchNo;
            ExpirDate = expirDate;
            IsSkinTest = isSkinTest;
            SkinTestSignChoseResult = skinTestSignChoseResult;
            IsFirstAid = isFirstAid;
            AntibioticPermission = antibioticPermission;
            PrescriptionPermission = prescriptionPermission;
            IsCriticalPrescription = isCriticalPrescription;
            HisUnit = hisUnit;
            HisDosageUnit = hisDosageUnit;
            HisDosageQty = hisDosageQty;
        }

        /// <summary>
        /// 更新开方操作
        /// </summary>
        public void Update(Prescribe obj)
        {
            IsOutDrug = obj.IsOutDrug;
            MedicineProperty = obj.MedicineProperty;
            ToxicProperty = obj.ToxicProperty;
            UsageCode = obj.UsageCode;
            UsageName = obj.UsageName;
            Speed = obj.Speed;
            LongDays = obj.LongDays;
            ActualDays = obj.ActualDays;
            DosageQty = obj.DosageQty;
            DefaultDosageQty = obj.DefaultDosageQty;
            QtyPerTimes = obj.QtyPerTimes;
            DosageUnit = obj.DosageUnit;
            Specification = obj.Specification;
            DefaultDosageUnit = obj.DefaultDosageUnit;
            Unpack = obj.Unpack;
            BigPackPrice = obj.BigPackPrice;
            BigPackFactor = obj.BigPackFactor;
            BigPackUnit = obj.BigPackUnit;
            SmallPackPrice = obj.SmallPackPrice;
            SmallPackUnit = obj.SmallPackUnit;
            SmallPackFactor = obj.SmallPackFactor;
            FrequencyCode = obj.FrequencyCode;
            FrequencyName = obj.FrequencyName;
            MedicalInsuranceCode = obj.MedicalInsuranceCode;
            FrequencyTimes = obj.FrequencyTimes;
            FrequencyUnit = obj.FrequencyUnit;
            FrequencyExecDayTimes = obj.FrequencyExecDayTimes;
            DailyFrequency = obj.DailyFrequency;
            PharmacyCode = obj.PharmacyCode;
            PharmacyName = obj.PharmacyName;
            FactoryName = obj.FactoryName;
            FactoryCode = obj.FactoryCode;
            BatchNo = obj.BatchNo;
            ExpirDate = obj.ExpirDate;
            IsSkinTest = obj.IsSkinTest;
            SkinTestResult = obj.SkinTestResult;
            SkinTestSignChoseResult = obj.SkinTestSignChoseResult;
            MaterialPrice = obj.MaterialPrice;
            IsBindingTreat = obj.IsBindingTreat;
            IsAmendedMark = obj.IsAmendedMark;
            IsAdaptationDisease = obj.IsAdaptationDisease;
            IsFirstAid = obj.IsFirstAid;
            AntibioticPermission = obj.AntibioticPermission;
            PrescriptionPermission = obj.PrescriptionPermission;
            IsCriticalPrescription = obj.IsCriticalPrescription;
            HisUnit = obj.HisUnit;
            HisDosageUnit = obj.HisDosageUnit;
            HisDosageQty = obj.HisDosageQty;
        }

        /// <summary>
        /// 更新开方操作
        /// </summary>
        public void Update(
            bool isOutDrug,
            string medicineProperty,
            string toxicProperty,
            string usageCode,
            string usageName,
            string speed,
            int longDays,
            int? actualDays,
            string dosageForm,
            decimal dosageQty,
            decimal defaultDosageQty,
            decimal? qtyPerTimes,
            string dosageUnit,
            string defaultDosageUnit,
            string specification,
            EMedicineUnPack unpack,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            string frequencyCode,
            string frequencyName,
            string medicalInsuranceCode,
            int? frequencyTimes,
            string frequencyUnit,
            string frequencyExecDayTimes,
            string dailyFrequency,
            string pharmacyCode,
            string pharmacyName,
            string factoryName,
            string factoryCode,
            string batchNo,
            DateTime? expirDate,
            bool? isSkinTest,
            bool? skinTestResult,
            ESkinTestSignChoseResult? skinTestSignChoseResult,
            decimal? materialPrice,
            bool? isBindingTreat,
            bool? isAmendedMark,
            bool? isAdaptationDisease,
            bool? isFirstAid,
            int antibioticPermission,
            int prescriptionPermission,
            bool isCriticalPrescription,
            string hisUnit,
            string hisDosageUnit,
            decimal hisDosageQty)
        {
            IsOutDrug = isOutDrug;
            MedicineProperty = medicineProperty;
            ToxicProperty = toxicProperty;
            UsageCode = usageCode;
            UsageName = usageName;
            Speed = speed;
            LongDays = longDays;
            ActualDays = actualDays;
            DosageForm = dosageForm;
            DosageQty = dosageQty;
            DefaultDosageQty = defaultDosageQty;
            QtyPerTimes = qtyPerTimes;
            DosageUnit = dosageUnit;
            Specification = specification;
            DefaultDosageUnit = defaultDosageUnit;
            Unpack = unpack;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackUnit = smallPackUnit;
            SmallPackFactor = smallPackFactor;
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            MedicalInsuranceCode = medicalInsuranceCode;
            FrequencyTimes = frequencyTimes;
            FrequencyUnit = frequencyUnit;
            FrequencyExecDayTimes = frequencyExecDayTimes;
            DailyFrequency = dailyFrequency;
            PharmacyCode = pharmacyCode;
            PharmacyName = pharmacyName;
            FactoryName = factoryName;
            FactoryCode = factoryCode;
            BatchNo = batchNo;
            ExpirDate = expirDate;
            IsSkinTest = isSkinTest;
            SkinTestResult = skinTestResult;
            SkinTestSignChoseResult = skinTestSignChoseResult;
            MaterialPrice = materialPrice;
            IsBindingTreat = isBindingTreat;
            IsAmendedMark = isAmendedMark;
            IsAdaptationDisease = isAdaptationDisease;
            IsFirstAid = isFirstAid;
            AntibioticPermission = antibioticPermission;
            PrescriptionPermission = prescriptionPermission;
            IsCriticalPrescription = isCriticalPrescription;
            HisUnit = hisUnit;
            HisDosageUnit = hisDosageUnit;
            HisDosageQty = hisDosageQty;
        }

        /// <summary>
        /// 更新医嘱分组药品的属性信息
        /// </summary>
        public void UpdateGroupAdviceProp(string frequencyCode, string frequencyName, int? frequencyTimes, string usageCode, string usageName, int longDays, int? actualDays, string medicalInsuranceCode, string frequencyExecDayTimes, string frequencyUnit, string dailyFrequency)
        {
            FrequencyCode = frequencyCode;
            FrequencyName = frequencyName;
            FrequencyTimes = frequencyTimes;
            UsageCode = usageCode;
            UsageName = usageName;
            LongDays = longDays;
            ActualDays = actualDays;
            MedicalInsuranceCode = medicalInsuranceCode;
            FrequencyExecDayTimes = frequencyExecDayTimes;
            DailyFrequency = dailyFrequency;
            FrequencyUnit = frequencyUnit;
        }


        /// <summary>
        /// 更新关键的HIS信息
        /// </summary>
        public void UpdatePartProp(
            EMedicineUnPack unpack,
            decimal bigPackPrice,
            int bigPackFactor,
            string bigPackUnit,
            decimal smallPackPrice,
            string smallPackUnit,
            int smallPackFactor,
            int antibioticPermission,
            int prescriptionPermission,
            int medicineId)
        {
            Unpack = unpack;
            BigPackPrice = bigPackPrice;
            BigPackFactor = bigPackFactor;
            BigPackUnit = bigPackUnit;
            SmallPackPrice = smallPackPrice;
            SmallPackUnit = smallPackUnit;
            SmallPackFactor = smallPackFactor;
            AntibioticPermission = antibioticPermission;
            PrescriptionPermission = prescriptionPermission;
            MedicineId = medicineId;
        }
    }
}
