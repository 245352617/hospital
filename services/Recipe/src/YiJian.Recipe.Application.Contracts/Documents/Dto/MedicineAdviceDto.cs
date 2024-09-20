using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using YiJian.DoctorsAdvices.Dto;
using YiJian.ECIS.ShareModel.Extensions;
using YiJian.Hospitals.Enums;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 打印的药品基本信息内容
    /// </summary>
    public class MedicineAdviceDto : BaseAdviceDto
    {

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary> 
        public string MedicineProperty { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二） 
        /// </summary> 
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗
        /// </summary> 
        public int ToxicLevel { get; set; }

        ///// <summary>
        ///// 是否危急处方  是:true ，否:false
        ///// </summary>
        //public bool IsCriticalPrescription { get; set; }

        /// <summary>
        /// 药理 0=普通药品,1=毒性药品,2=麻醉、精一药品，3=精二类，4=放射性，5=贵重药品，6=妊娠药品，7=狂犬疫苗
        /// </summary> 
        public string ToxicLevelText => ToxicLevel == 1 ? "毒药" : ToxicLevel == 2 ? "麻、精一" : ToxicLevel == 3 ? "精二" : IsCriticalPrescription ? "急诊" : "";

        /// <summary>
        /// 药理
        /// </summary>
        [JsonIgnore]
        public ToxicDto Toxic
        {
            get
            {
                if (ToxicProperty.IsNullOrEmpty()) return null;
                return JsonConvert.DeserializeObject<ToxicDto>(ToxicProperty);
            }
        }

        /// <summary>
        /// 是否是限制药物（精、毒、麻）
        /// </summary>
        public bool IsNonPreferredDrugs
        {
            get
            {
                if (Toxic == null) return false;
                return (Toxic.IsDrunk.HasValue && Toxic.IsDrunk.Value)
                    || (Toxic.IsHighRisk.HasValue && Toxic.IsHighRisk.Value)
                    || (Toxic.IsAnaleptic.HasValue && Toxic.IsAnaleptic.Value)
                    || (Toxic.ToxicLevel.HasValue && (Toxic.ToxicLevel.Value == 1 || Toxic.ToxicLevel.Value == 2))
                    ;
            }
        }

        /// <summary>
        /// 用法编码
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称 
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 滴速
        /// </summary> 
        public string Speed { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int LongDays { get; set; }

        /// <summary>
        /// 实际天数
        /// </summary> 
        public int? ActualDays { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary> 
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 频次码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 在一个周期内执行的次数
        /// </summary> 
        public int? FrequencyTimes { get; set; }

        /// <summary>
        /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
        /// </summary> 
        public string FrequencyUnit { get; set; }

        /// <summary>
        /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
        /// </summary> 
        public string FrequencyExecDayTimes { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }

        /// <summary>
        /// 厂家
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary> 
        public string BatchNo { get; set; }

        /// <summary>
        /// 是否皮试 false=不需要皮试 true=需要皮试
        /// </summary> 
        public bool? IsSkinTest { get; set; }

        /// <summary>
        /// 皮试结果:false=阴性 ture=阳性
        /// </summary> 
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        public string SkinTestSignChoseResultText
        {
            get
            {
                if (SkinTestSignChoseResult == null) return "";
                return SkinTestSignChoseResult == ESkinTestSignChoseResult.Yes ? "【皮试】" : "【免皮试】";
            }
        }

        /// <summary>
        /// 耗材金额
        /// </summary> 
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 是否医保适应症
        /// </summary> 
        public bool? IsAdaptationDisease { get; set; }

        /// <summary>
        /// 是否是急救药
        /// </summary> 
        public bool? IsFirstAid { get; set; }

        /// <summary>
        /// 抗生素权限
        /// </summary> 
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary> 
        public int PrescriptionPermission { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary>
        public int LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary> 
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        /// <summary>
        /// 记账标记
        /// </summary>
        public string RestrictedDrugsText
        {
            get
            {
                if (RestrictedDrugs == null || (int)RestrictedDrugs.Value == 0) return "";
                return "(" + RestrictedDrugs.GetDescription() + ")";
            }
        }
        /// <summary>
        /// 成组的医嘱号，用于和成组药品打印时的关联
        /// </summary>
        public string GroupMedicineRecipeNo => RecipeNo + RecipeGroupNo;

        /// <summary>
        /// 名称和药品组名称拼接
        /// </summary>
        public string GroupName { get; set; }
        /// <summary>
        /// 是否危急处方  是:true ，否:false , 未处理
        /// </summary>
        public bool IsCriticalPrescription { get; set; }

        public string IsCriticalPrescriptionText
        {
            get
            {
                if (IsCriticalPrescription) return "是";
                return "否";
            }
        }


        public int EntryId { get; set; }
    }


}
