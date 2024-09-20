using Newtonsoft.Json;

namespace YiJian.ECIS.ShareModel.DDPs.Dto;

/// <summary>
/// 药品处方
/// </summary>
public class Prescribe4HisDto : DoctorAdvices4HisDto
{
    /// <summary>
    /// 药品表主键
    /// </summary>
    [JsonProperty("prescribeId")]
    public string PrescribeId { get; set; }

    /// <summary>
    /// 领用数量
    /// </summary>
    [JsonProperty("recieveQty")]
    public decimal RecieveQty { get; set; }

    ///// <summary>
    ///// 医嘱组号(对应医嘱表中的RecipeNo)
    ///// </summary>
    //[JsonProperty("groupNo")]
    //public string GroupNo { get; set; }

    ///// <summary>
    ///// 组内序号（对应医嘱表中的RecipeGroupNo）
    ///// </summary>
    //[JsonProperty("groupSubNo")]
    //public int GroupSubNo { get; set; } 

    /// <summary>
    /// 总费用(对应医嘱表中的Amount字段)
    /// </summary>
    [JsonProperty("amount")]
    public decimal Amount { get; set; }

    /// <summary>
    /// 单位
    /// </summary>
    [JsonProperty("unit")]
    public string Unit { get; set; }

    /// <summary>
    /// 医嘱类型：临嘱、长嘱、出院带药等
    /// </summary>
    [JsonProperty("prescribeTypeName")]
    public string PrescribeTypeName { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    [JsonProperty("startTime")]
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary>
    [JsonProperty("endTime")]
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary>
    [JsonProperty("recieveUnit")]
    public string RecieveUnit { get; set; }

    /// <summary>
    /// 是否自备药：false=非自备药,true=自备药
    /// </summary>
    [JsonProperty("isOutDrug")]
    public bool IsOutDrug { get; set; }

    /// <summary>
    /// 药物属性：西药、中药、西药制剂、中药制剂
    /// </summary>
    [JsonProperty("medicineProperty")]
    public string MedicineProperty { get; set; }

    /// <summary>
    /// 药理等级：如（毒、麻、精一、精二） 
    /// </summary>
    [JsonProperty("toxicProperty")]
    public string ToxicProperty { get; set; }

    /// <summary>
    /// 用法编码
    /// </summary>
    [JsonProperty("usageCode")]
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary>
    [JsonProperty("usageName")]
    public string UsageName { get; set; }

    /// <summary>
    /// 滴速
    /// </summary>
    [JsonProperty("speed")]
    public string Speed { get; set; }

    /// <summary>
    /// 开药天数
    /// </summary>
    [JsonProperty("longDays")]
    public int LongDays { get; set; }

    /// <summary>
    /// 实际天数
    /// </summary>
    [JsonProperty("actualDays")]
    public int? ActualDays { get; set; }

    /// <summary>
    /// 每次剂量
    /// </summary>
    [JsonProperty("dosageQty")]
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 默认规格剂量
    /// </summary>
    [JsonProperty("defaultDosageQty")]
    public decimal DefaultDosageQty { get; set; }

    /// <summary>
    /// 每次用量
    /// </summary>
    [JsonProperty("qtyPerTimes")]
    public decimal? QtyPerTimes { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    [JsonProperty("dosageUnit")]
    public string DosageUnit { get; set; }

    /// <summary>
    /// 默认规格剂量单位
    /// </summary> 
    [JsonProperty("defaultDosageUnit")]
    public string DefaultDosageUnit { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    [JsonProperty("unpack")]
    public int Unpack { get; set; }

    /// <summary>
    /// 包装价格
    /// </summary>
    [JsonProperty("bigPackPrice")]
    public decimal BigPackPrice { get; set; }

    /// <summary>
    /// 大包装系数(拆零系数)
    /// </summary>
    [JsonProperty("bigPackFactor")]
    public int BigPackFactor { get; set; }

    /// <summary>
    /// 大包装单位
    /// </summary>
    [JsonProperty("bigPackUnit")]
    public string BigPackUnit { get; set; }

    /// <summary>
    /// 小包装单价
    /// </summary>
    [JsonProperty("smallPackPrice")]
    public decimal SmallPackPrice { get; set; }

    /// <summary>
    /// 小包装单位
    /// </summary> 
    [JsonProperty("smallPackUnit")]
    public string SmallPackUnit { get; set; }

    /// <summary>
    /// 小包装系数(拆零系数)
    /// </summary>
    [JsonProperty("smallPackFactor")]
    public int SmallPackFactor { get; set; }

    /// <summary>
    /// 频次编码
    /// </summary>
    [JsonProperty("frequencyCode")]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    [JsonProperty("frequencyName")]
    public string FrequencyName { get; set; }

    /// <summary>
    /// 在一个周期内执行的次数
    /// </summary>
    [JsonProperty("frequencyTimes")]
    public int? FrequencyTimes { get; set; }

    /// <summary>
    /// 周期单位，比如1D=每天，1W=每周，2D=隔天执行，1H=每小时或者每半小时
    /// </summary>
    [JsonProperty("frequencyUnit")]
    public string FrequencyUnit { get; set; }

    /// <summary>
    /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
    /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
    /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
    /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
    /// </summary>
    [JsonProperty("frequencyExecDayTimes")]
    public string FrequencyExecDayTimes { get; set; }

    /// <summary>
    /// HIS频次编码
    /// </summary>
    [JsonProperty("dailyFrequency")]
    public string DailyFrequency { get; set; }

    /// <summary>
    /// 药房编码
    /// </summary>
    [JsonProperty("pharmacyCode")]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房名称
    /// </summary>
    [JsonProperty("pharmacyName")]
    public string PharmacyName { get; set; }

    /// <summary>
    /// 厂家名称
    /// </summary>
    [JsonProperty("factoryName")]
    public string FactoryName { get; set; }

    /// <summary>
    /// 厂家代码
    /// </summary>
    [JsonProperty("factoryCode")]
    public string FactoryCode { get; set; }

    /// <summary>
    /// 药品产地
    /// </summary>
    [JsonProperty("factoryAddr")]
    public string FactoryAddr { get; set; }

    /// <summary>
    /// 批次号
    /// </summary>
    [JsonProperty("batchNo")]
    public string BatchNo { get; set; }

    /// <summary>
    /// 失效期
    /// </summary>
    [JsonProperty("expirDate")]
    public DateTime? ExpirDate { get; set; }

    /// <summary>
    /// 是否皮试 false=不需要皮试 true=需要皮试
    /// </summary>
    [JsonProperty("isSkinTest")]
    public bool? IsSkinTest { get; set; }

    /// <summary>
    /// 耗材金额
    /// </summary>
    [JsonProperty("materialPrice")]
    public decimal? MaterialPrice { get; set; }

    /// <summary>
    /// 是否抢救后补：false=非抢救后补，true=抢救后补
    /// </summary>
    [JsonProperty("isAmendedMark")]
    public bool? IsAmendedMark { get; set; }

    /// <summary>
    /// 是否医保适应症
    /// </summary>
    [JsonProperty("isAdaptationDisease")]
    public bool? IsAdaptationDisease { get; set; }

    /// <summary>
    /// 是否是急救药
    /// </summary>
    [JsonProperty("isFirstAid")]
    public bool? IsFirstAid { get; set; }

    /// <summary>
    /// 抗生素权限
    /// </summary>
    [JsonProperty("antibioticPermission")]
    public int AntibioticPermission { get; set; }

    /// <summary>
    /// 处方权
    /// </summary>
    [JsonProperty("prescriptionPermission")]
    public int PrescriptionPermission { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary>
    [JsonProperty("specification")]
    public string Specification { get; set; }

    /// <summary>
    /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
    /// </summary>
    [JsonProperty("limitType")]
    public int LimitType { get; set; }

    /// <summary>
    /// 药品性质（对应prescribe的RestrictedDrugs字段）
    /// </summary> 
    [JsonProperty("restrictedDrugs")]
    public int? RestrictedDrugs { get; set; }

    /// <summary>
    /// 儿童价格
    /// </summary>
    [JsonProperty("childrenPrice")]
    public decimal? ChildrenPrice { get; set; }

    /// <summary>
    /// 批发价格
    /// </summary>
    [JsonProperty("fixPrice")]
    public decimal? FixPrice { get; set; }

    /// <summary>
    /// 零售价格
    /// </summary>
    [JsonProperty("retPrice")]
    public decimal? RetPrice { get; set; }

    /// <summary>
    /// 药品id （新，重要）
    /// </summary> 
    [JsonProperty("medicineId")]
    public int MedicineId { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    [JsonProperty("medicalInsuranceCode")]
    public string MedicalInsuranceCode { get; set; }

    /// <summary>
    /// 是否危急处方  是:true ，否:false , 未处理
    /// </summary>
    [JsonProperty("isCriticalPrescription")]
    public bool IsCriticalPrescription { get; set; }

    /// <summary>
    /// 单号序号，同一个处方单，单号从1开始递增
    /// </summary>
    [JsonProperty("sequenceNo")]
    public int SequenceNo { get; set; }
}
