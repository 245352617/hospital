using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.Medicines;


/// <summary>
/// 药品字典
/// </summary>
[Comment("药品字典")]
public class Medicine : FullAuditedAggregateRoot<int>, IIsActive
{
    #region 属性

    /// <summary>
    /// 药品编码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("药品编码")]
    public string MedicineCode { get; set; }

    string _name;

    /// <summary>
    /// 药品名称
    /// </summary>
    [Required]
    [StringLength(200)]
    [Comment("药品名称")]
    public string MedicineName
    {
        get { return _name; }
        set
        {
            if (value != _name)
            {
                _name = value;
                PyCode = _name.FirstLetterPY();
                WbCode = _name.FirstLetterWB();
            }
        }
    }

    /// <summary>
    /// 学名
    /// </summary>
    [StringLength(200)]
    [Comment("学名")]
    public string ScientificName { get; set; }

    string _alias;

    /// <summary>
    /// 别名
    /// </summary>
    [StringLength(200)]
    [Comment("别名")]
    public string Alias
    {
        get { return _alias; }
        set
        {
            if (value != _alias)
            {
                _alias = value;
                AliasPyCode = _alias.FirstLetterPY(50);
                AliasWbCode = _alias.FirstLetterWB(50);
            }
        }
    }

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
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔
    /// </summary> 
    [StringLength(50)]
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
    [StringLength(20)]
    [Comment("剂量单位")]
    public string DosageUnit { get; set; }

    #endregion 剂量

    #region 基本(单位)价格

    /// <summary>
    /// 基本单位
    /// </summary>
    //[Required]
    [StringLength(20)]
    [Comment("基本单位")]
    public string Unit { get; set; }

    /// <summary>
    /// 基本单位价格
    /// </summary>
    [Comment("基本单位价格")]
    public decimal Price { get; set; }

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
    // [Required]
    [Comment("大包装量(大包装系数)")]
    public int BigPackFactor { get; set; }

    /// <summary>
    /// 大包装单位
    /// </summary>
    // [Required]
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
    // [Required]
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
    /// 医保类型：0自费,1甲类,2乙类，3丙类
    /// </summary>
    [Comment("医保类型：0自费,1甲类,2乙类，3丙类")]
    [Obsolete("与InsuranceCode重复属性，且此处不应该使用枚举硬编码")]
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 医保类型代码
    /// </summary>
    [Comment("医保类型代码")]
    public int InsuranceCode { get; set; }

    /// <summary>
    /// 医保类型名称
    /// </summary>
    [Comment("医保类型名称")]
    [StringLength(50)]
    public string InsuranceName { get; set; }

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

    // /// <summary>
    // /// 批次号
    // /// </summary>
    // [StringLength(20)]
    // [Comment("批次号")]
    // public string BatchNo { get; set; }
    //
    // /// <summary>
    // /// 失效期
    // /// </summary>
    // [StringLength(20)]
    // [Comment("失效期")]
    // public DateTime? ExpirDate { get; set; }

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
    /// 备注
    /// </summary>
    [StringLength(500)]
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
    // [Required]
    [StringLength(50)]
    [Comment("规格")]
    public string Specification { get; set; }

    /// <summary>
    /// 剂型
    /// </summary>
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
    [StringLength(20)]
    [Comment("药房代码")]
    public string PharmacyCode { get; set; }

    /// <summary>
    /// 药房
    /// </summary>
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

    // /// <summary>
    // /// 库存
    // /// </summary>
    // [Comment("库存")]
    // public int Stock { get; set; }

    /// <summary>
    /// 基药标准  01国基，02省基，03市基，04基药，05中草药，06非基药
    /// </summary>
    [StringLength(20)]
    [Comment("基药标准 01国基，02省基，03市基，04基药，05中草药，06非基药")]
    public string BaseFlag { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    [StringLength(50)]
    [Comment("医保编码")]
    public string MedicalInsuranceCode { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    [Comment("平台标识")]
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    [Comment("门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分")]
    public MedicineUnPack Unpack { get; set; }

    #endregion 属性

    /// <summary>
    /// 是否已经全量同步过Recipes库，默认false=没有同步过。同步过的只做差量同步，否则全量同步
    /// </summary>
    [Comment("是否已经全量同步过Recipes库")]
    public bool IsSyncToReciped { get; set; } = false;

    ///// <summary>
    ///// （急诊处方标志）1.急诊 0.普通 
    ///// </summary>
    [Comment("（急诊处方标志）1.急诊 0.普通")]
    public decimal EmergencySign { get; set; }

    #region Modify

    /// <summary>
    /// 更新操作同步信息
    /// </summary>
    /// <param name="isSyncToReciped"></param>
    public void UpdateSync(bool isSyncToReciped)
    {
        IsSyncToReciped = isSyncToReciped;
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">药品名称</param>
    /// <param name="alias">别名</param>
    /// <param name="medicineProperty"></param>
    /// <param name="basicUnitPrice">基本单位价格</param>
    /// <param name="remark">备注</param>
    /// <param name="isEmergency">急救药</param>
    /// <param name="usage">药物用途</param>
    /// <param name="platformType"></param>
    /// <param name="unpack"></param>
    public void Modify([NotNull] string name, // 药品名称
        string alias, // 别名
        string medicineProperty,
        decimal basicUnitPrice, // 基本单位价格
        string remark, // 备注
        bool? isEmergency, // 是否急救药
        string usage, // 药物用途
        PlatformType platformType, MedicineUnPack unpack //是否急救
    )
    {
        //药品名称
        MedicineName = name;

        //别名
        Alias = alias;

        //中药分类
        MedicineProperty = medicineProperty;

        //基本单位价格
        Price = basicUnitPrice;

        //备注
        Remark = remark;

        //急救药
        IsFirstAid = isEmergency;

        //药物用途
        UsageCode = usage;
        PlatformType = platformType;
        Unpack = unpack;
    }


    /// <summary>
    /// 同步HisMedicine的数据
    /// </summary>
    /// <param name="model"></param>
    public void SyncData(HISMedicine model)
    {

        MedicineCode = model.MedicineCode.ToString();
        MedicineName = model.MedicineName;
        ScientificName = model.ScientificName;
        Alias = model.Alias;
        AliasPyCode = model.AliasPyCode;
        AliasWbCode = model.AliasWbCode;
        PyCode = model.PyCode;
        WbCode = model.WbCode;
        MedicineProperty = ""; // model.MedicineProperty;
        DefaultDosage = (double)model.DefaultDosage;
        DosageQty = model.DosageQty;
        DosageUnit = model.DosageUnit;
        Unit = model.Unit;
        Price = model.Price;
        BigPackPrice = model.BigPackPrice;
        BigPackFactor = (int)model.BigPackFactor;
        BigPackUnit = model.BigPackUnit;
        SmallPackPrice = model.SmallPackPrice;
        SmallPackUnit = model.SmallPackUnit;
        SmallPackFactor = (int)model.SmallPackFactor;
        ChildrenPrice = model.ChildrenPrice;
        FixPrice = model.FixPrice;
        RetPrice = model.RetPrice;
        InsuranceCode = (int)model.InsuranceCode;
        InsuranceName = model.InsuranceName;
        FactoryName = model.FactoryName;
        FactoryCode = model.FactoryCode.ToString();
        Weight = (double?)model.Weight;
        WeightUnit = model.WeightUnit;
        VolumeUnit = model.VolumeUnit;
        Remark = model.Remark;
        IsSkinTest = model.IsSkinTest.HasValue ? (model.IsSkinTest.Value == 1) : (bool?)null;
        IsDrunk = model.IsDrunk.HasValue ? (model.IsDrunk.Value == 1 ? true : false) : (bool?)null;
        IsCompound = model.IsCompound.IsNullOrEmpty() ? (bool?)null : (model.IsCompound.Trim() == "1");
        ToxicLevel = (int?)model.ToxicLevel;
        IsHighRisk = model.IsHighRisk.IsNullOrEmpty() ? (bool?)null : (model.IsHighRisk.Trim() == "1" ? true : false);
        IsRefrigerated = model.IsRefrigerated.IsNullOrEmpty() ? (bool?)null : (model.IsRefrigerated.Trim() == "1");
        IsTumour = model.IsTumour.IsNullOrEmpty() ? (bool?)null : (model.IsTumour.Trim() == "1");
        AntibioticLevel = model.AntibioticLevel;
        IsPrecious = model.IsPrecious.HasValue ? (model.IsPrecious.Value == 1) : (bool?)null;
        IsInsulin = model.IsInsulin.HasValue ? (model.IsInsulin.Value == 1) : (bool?)null;
        IsAnaleptic = model.IsAnaleptic.HasValue ? (model.IsAnaleptic.Value == 1) : (bool?)null;
        IsAllergyTest = model.IsAllergyTest.HasValue ? (model.IsAllergyTest.Value == 1) : (bool?)null;
        IsLimited = model.IsLimited.IsNullOrEmpty() ? (bool?)null : (model.IsLimited.Trim() == "1");
        LimitedNote = model.LimitedNote;
        Specification = model.Specification;
        DosageForm = model.DosageForm;
        ExecDeptCode = model.ExecDeptCode.ToString();
        ExecDeptName = model.ExecDeptName;
        UsageCode = model.UsageCode.HasValue ? model.UsageCode.Value.ToString() : "";
        UsageName = model.UsageName;
        FrequencyCode = model.FrequencyCode;
        FrequencyName = model.FrequencyName;
        IsActive = model.IsActive == 1;
        IsFirstAid = model.IsFirstAid == 1;
        PharmacyCode = model.PharmacyCode;
        PharmacyName = model.PharmacyName;
        AntibioticPermission = (int)model.AntibioticPermission;
        PrescriptionPermission = (int)model.PrescriptionPermission;
        BaseFlag = model.BaseFlag;
        Unpack = model.Unpack;
        IsSyncToReciped = false;
        EmergencySign = model.EmergencySign;
    }

    #endregion Modify
}