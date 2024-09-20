namespace YiJian.ECIS.ShareModel.DDPs.Responses;

/// <summary>
/// Ddp His返回的药品清单信息
/// </summary>
public class PKUHisMedicineResponse
{
    public string Id { get; set; }

    /// <summary>
    /// 药品库存Id
    /// </summary>
    public string InvId { get; set; }

    /// <summary>
    /// 药品编码
    /// </summary>
    public string MedicineCode { get; set; }

    /// <summary>
    /// 药品名称
    /// </summary>
    public string MedicineName { get; set; }

    /// <summary>
    /// 学名
    /// </summary>
    public string ScientificName { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string Alias { get; set; }

    /// <summary>
    /// 别名拼音
    /// </summary>
    public string AliasPyCode { get; set; }

    /// <summary>
    /// 别名五笔码
    /// </summary>
    public string AliasWbCode { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCodeLen { get; set; }

    /// <summary>
    /// 五笔
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 药物属性：西药、中药、西药制剂、中药制剂
    /// </summary>
    public string MedicineProperty { get; set; }

    /// <summary>
    /// 默认剂量
    /// </summary> 
    public string DefaultDosage { get; set; }

    /// <summary>
    /// 剂量
    /// </summary> 
    public string DosageQty { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public string DosageUnit { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 基本单位价格
    /// </summary> 
    public string Price { get; set; }

    /// <summary>
    /// 包装价格
    /// </summary> 
    public string BigPackPrice { get; set; }

    /// <summary>
    /// 大包装量(大包装系数)
    /// </summary> 
    public string BigPackFactor { get; set; }

    /// <summary>
    /// 包装单位
    /// </summary>
    public string BigPackUnit { get; set; }

    /// <summary>
    /// 小包装单价
    /// </summary> 
    public string SmallPackPrice { get; set; }

    /// <summary>
    /// 小包装单位
    /// </summary>
    public string SmallPackUnit { get; set; }

    /// <summary>
    /// 小包装量(小包装系数)
    /// </summary> 
    public string SmallPackFactor { get; set; }

    /// <summary>
    /// 儿童价格
    /// </summary> 
    public string? ChildrenPrice { get; set; }

    /// <summary>
    /// 批发价格
    /// </summary> 
    public string? FixPrice { get; set; }

    /// <summary>
    /// 零售价格
    /// </summary> 
    public string? RetPrice { get; set; }

    /// <summary>
    /// 医保类型：0自费,1甲类,2乙类，3丙类
    /// </summary>
    public string InsuranceType { get; set; }

    /// <summary>
    /// 医保类型代码
    /// </summary> 
    public string InsuranceCode { get; set; }

    /// <summary>
    /// 医保类型名称
    /// </summary>
    public string InsuranceName { get; set; }

    /// <summary>
    /// 医保支付比例
    /// </summary> 
    public string? InsurancePayRate { get; set; }

    /// <summary>
    /// 厂家名称
    /// </summary>
    public string FactoryName { get; set; }

    /// <summary>
    /// 厂家代码
    /// </summary>
    public string FactoryCode { get; set; }


    /// <summary>
    /// 重量
    /// </summary> 
    public string? Weight { get; set; }

    /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

    /// <summary>
    /// 体积
    /// </summary> 
    public string? Volume { get; set; }

    /// <summary>
    /// 体积单位
    /// </summary>
    public string VolumeUnit { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

    /// <summary>
    /// 皮试药
    /// </summary> 
    public string? IsSkinTest { get; set; }

    /// <summary>
    /// 复方药
    /// </summary> 
    public string? IsCompound { get; set; }

    /// <summary>
    /// 麻醉药
    /// </summary> 
    public string? IsDrunk { get; set; }

    /// <summary>
    /// 精神药  0 普通,1 毒,2 麻，3 精神
    /// </summary>
    public string? ToxicLevel { get; set; }

    /// <summary>
    /// 高危药
    /// </summary> 
    public string? IsHighRisk { get; set; }

    /// <summary>
    /// 冷藏药
    /// </summary> 
    public string? IsRefrigerated { get; set; }

    /// <summary>
    /// 肿瘤药
    /// </summary> 
    public string? IsTumour { get; set; }

    ///// <summary>
    ///// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
    ///// </summary>
    //public string? AntibioticLevel { get; set; }

    /// <summary>
    /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
    /// </summary> 
    public string? AntibioticLevel { get; set; }

    /// <summary>
    /// 贵重药
    /// </summary> 
    public string? IsPrecious { get; set; }

    /// <summary>
    /// 胰岛素
    /// </summary> 
    public string? IsInsulin { get; set; }

    /// <summary>
    /// 兴奋剂
    /// </summary>
    public string? IsAnaleptic { get; set; }

    /// <summary>
    /// 试敏药
    /// </summary> 
    public string? IsAllergyTest { get; set; }

    /// <summary>
    /// 限制性用药标识
    /// </summary> 
    public string? IsLimited { get; set; }

    ///// <summary>
    ///// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
    ///// </summary> 
    //[JsonIgnore]
    //public string LimitType  
    //{
    //    get
    //    {
    //        if (IsLimited.HasValue)
    //        {
    //            return IsLimited.Value ? 1 : 2;
    //        }
    //        return 0;
    //    }
    //}

    /// <summary>
    /// 限制性用药描述
    /// </summary>
    public string LimitedNote { get; set; }

    /// <summary>
    /// 规格
    /// </summary>
    public string Specification { get; set; }

    /// <summary>
    /// 剂型
    /// </summary>
    public string DosageForm { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary>
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室
    /// </summary>
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 默认用法编码
    /// </summary>
    public string UsageCode { get; set; }

    /// <summary>
    /// 默认用法名称
    /// </summary>
    public string UsageName { get; set; }

    /// <summary>
    /// 默认频次编码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 默认频次名称
    /// </summary>
    public string FrequencyName { get; set; }

    /// <summary>
    /// 在一个周期内执行的次数
    /// </summary> 
    public string? FrequencyTimes { get; set; }

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
    /// 是否启用
    /// </summary> 
    public string? IsActive { get; set; }

    /// <summary>
    /// 急救药
    /// </summary> 
    public string? IsFirstAid { get; set; }

    /// <summary>
    /// 药房代码
    /// </summary>
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房
    /// </summary>
    public string PharmacyName { get; set; }

    /// <summary>
    /// 抗生素权限
    /// </summary> 
    public string AntibioticPermission { get; set; }

    /// <summary>
    /// 处方权
    /// </summary> 
    public string PrescriptionPermission { get; set; }


    /// <summary>
    /// 基药标准  N普通,Y国基,P省基,C市基
    /// </summary>
    public string BaseFlag { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    public string Unpack { get; set; }

    /// <summary>
    /// 分类编码-院前使用
    /// </summary>
    public string CategoryCode { get; set; } = "Medicine";

    /// <summary>
    /// 分类名称-院前使用
    /// </summary>
    public string CategoryName { get; set; } = "药物";

    /// <summary>
    /// HIS频次编码
    /// </summary>
    public string DailyFrequency { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    public string MedicalInsuranceCode { get; set; }

    /// <summary>
    /// 医保二级编码
    /// </summary>
    public string YBInneCode { get; set; }

    /// <summary>
    /// 医保统一名称
    /// </summary>
    public string MeducalInsuranceName { get; set; }

    ///// <summary>
    ///// （急诊处方标志）1.急诊 0.普通 
    ///// </summary>
    public string EmergencySign { get; set; }

    /// <summary>
    /// 库存
    /// </summary>  
    public decimal? Qty { get; set; }
}
