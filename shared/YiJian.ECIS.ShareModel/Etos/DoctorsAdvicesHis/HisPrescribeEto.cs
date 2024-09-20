using System.ComponentModel.DataAnnotations;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 药方ETO
/// </summary>
public class HisPrescribeEto
{
    /// <summary>
    /// HIS医嘱号
    /// </summary> 
    [StringLength(36)]
    [Required]
    public string HisOrderNo { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 医嘱Id
    /// </summary> 
    public Guid DoctorsAdviceId { get; set; }

    /// <summary>
    /// 是否自备药：false=非自备药,true=自备药
    /// </summary> 
    [Required]
    public bool IsOutDrug { get; set; }

    /// <summary>
    /// 药物属性：西药、中药、西药制剂、中药制剂
    /// </summary> 
    [Required, StringLength(20)]
    public string MedicineProperty { get; set; }

    /// <summary>
    /// 药理等级：如（毒、麻、精一、精二）
    /// </summary> 
    [StringLength(5000)]
    public string ToxicProperty { get; set; }
    //public Dictionary<string,object> ToxicProperty { get; set; }

    /// <summary>
    /// 用法编码
    /// </summary> 
    [Required, StringLength(20)]
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary> 
    [Required, StringLength(20)]
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
    /// 每次剂量
    /// </summary> 
    [Required]
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary> 
    [Required, StringLength(20)]
    public string DosageUnit { get; set; }

    /// <summary>
    /// 每次用量
    /// </summary> 
    public decimal? QtyPerTimes { get; set; }

    /// <summary>
    /// 领量(数量)
    /// </summary> 
    [Required]
    public decimal RecieveQty { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary> 
    [Required, StringLength(20)]
    public string RecieveUnit { get; set; }

    /// <summary>
    /// 包装规格
    /// </summary> 
    [StringLength(200)]
    public string Specification { get; set; }

    /// <summary>
    /// 频次码
    /// </summary> 
    [Required, StringLength(20)]
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次
    /// </summary> 
    [StringLength(20)]
    public string FrequencyName { get; set; }

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
    /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。日时间点只有一个的时候，格式为：HH:mm:ss.fff。日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。周时间点一个到多个的时候，格式为：周[一丨二丨三丨四丨五丨六丨日丨天]:HH:mm，以分号（;）分割。 
    /// </summary> 
    [StringLength(500)]
    public string FrequencyExecDayTimes { get; set; }

    /// <summary>
    /// 药房编码
    /// </summary> 
    [Required, StringLength(20)]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房
    /// </summary> 
    [Required, StringLength(50)]
    public string PharmacyName { get; set; }

    /// <summary>
    /// 是否皮试 false=不需要皮试 true=需要皮试
    /// </summary> 
    public bool? IsSkinTest { get; set; }

    /// <summary>
    /// 耗材金额
    /// </summary> 
    public decimal? MaterialPrice { get; set; }

    /// <summary>
    /// 是否抢救后补：false=非抢救后补，true=抢救后补
    /// </summary> 
    public bool? IsAmendedMark { get; set; }

    /// <summary>
    /// 是否医保适应症
    /// </summary> 
    public bool? IsAdaptationDisease { get; set; }

    /// <summary>
    /// 是否是急救药
    /// </summary> 
    public bool? IsFirstAid { get; set; }

    ///// <summary>
    ///// 皮试结果:false=阴性 ture=阳性
    ///// </summary> 
    //public bool? SkinTestResult { get; set; }

    ///// <summary>
    ///// 开始时间
    ///// </summary> 
    //public DateTime? StartTime { get; set; }

    ///// <summary>
    ///// 结束时间
    ///// </summary> 
    //public DateTime? EndTime { get; set; }

    ///// <summary>
    ///// 实际天数
    ///// </summary> 
    //public int? ActualDays { get; set; }

    ///// <summary>
    ///// 用于判断关联耗材是否手动删除
    ///// </summary> 
    //public bool? IsBindingTreat { get; set; }

    ///// <summary>
    ///// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    ///// </summary> 
    //public int Unpack { get; set; }

    ///// <summary>
    ///// 包装价格
    ///// </summary> 
    //public decimal BigPackPrice { get; set; }

    ///// <summary>
    ///// 大包装系数(拆零系数)
    ///// </summary> 
    //public int BigPackFactor { get; set; }

    ///// <summary>
    ///// 包装单位
    ///// </summary> 
    //public string BigPackUnit { get; set; }

    ///// <summary>
    ///// 小包装单价
    ///// </summary> 
    //public decimal SmallPackPrice { get; set; }

    ///// <summary>
    ///// 小包装单位
    ///// </summary>  
    //public string SmallPackUnit { get; set; }

    ///// <summary>
    ///// 小包装系数(拆零系数)
    ///// </summary> 
    //public int SmallPackFactor { get; set; }

    ///// <summary>
    ///// 厂家代码
    ///// </summary> 
    //public string FactoryCode { get; set; }

    ///// <summary>
    ///// 厂家
    ///// </summary> 
    //public string FactoryName { get; set; }

    ///// <summary>
    ///// 批次号
    ///// </summary> 
    //public string BatchNo { get; set; }

    ///// <summary>
    ///// 失效期
    ///// </summary> 
    //public DateTime? ExpirDate { get; set; }

    ///// <summary>
    ///// 抗生素权限
    ///// </summary> 
    //public int AntibioticPermission { get; set; }

    ///// <summary>
    ///// 处方权
    ///// </summary>
    //public int PrescriptionPermission { get; set; }




}
