using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.MasterData.Medicines;

/// <summary>
/// 药品字典 读取输出
/// </summary>
[Serializable]
public class MedicineData : IEqualityComparer<MedicineData>
{
    public int Id { get; set; }

    /// <summary>
    /// 药品编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 药品名称
    /// </summary>
    public string Name { get; set; }

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
    public int PyCodeLen { get { return PyCode.Length; } }

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
    public double DefaultDosage { get; set; }

    /// <summary>
    /// 剂量
    /// </summary>
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public string DosageUnit { get; set; }

    /// <summary>
    /// 基本单位
    /// </summary>
    public string Unit { get; set; }

    #region 冗余字段，给前端用，前端改变了之前的数据，所以用这几个作为不能改变的数据源

    /// <summary>
    /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
    /// </summary>  
    public string HisUnit
    {
        get
        {
            return Unit;
        }
    }

    /// <summary>
    /// HIS的一次剂量数量（HIS标准剂量单位的数量）
    /// </summary>  
    public string HisDosageUnit
    {
        get
        {
            return DosageUnit;
        }
    }

    /// <summary>
    /// HIS的一次剂量数量（HIS标准剂量单位的数量）
    /// </summary>  
    public decimal HisDosageQty
    {
        get
        {
            return DosageQty;
        }
    }

    #endregion

    /// <summary>
    /// 基本单位价格
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 包装价格
    /// </summary>
    public decimal BigPackPrice { get; set; }

    /// <summary>
    /// 大包装量(大包装系数)
    /// </summary>
    public int BigPackFactor { get; set; }

    /// <summary>
    /// 包装单位
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
    /// 儿童价格
    /// </summary>
    public decimal? ChildrenPrice { get; set; }

    /// <summary>
    /// 批发价格
    /// </summary>
    public decimal? FixPrice { get; set; }

    /// <summary>
    /// 零售价格
    /// </summary>
    public decimal? RetPrice { get; set; }

    /// <summary>
    /// 医保类型：0自费,1甲类,2乙类，3丙类
    /// </summary>
    public InsuranceCatalog InsuranceType { get; set; }

    /// <summary>
    /// 医保类型代码
    /// </summary>
    public int InsuranceCode { get; set; }

    /// <summary>
    /// 医保支付比例
    /// </summary>
    public int? InsurancePayRate { get; set; }

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
    public double? Weight { get; set; }

    /// <summary>
    /// 重量单位
    /// </summary>
    public string WeightUnit { get; set; }

    /// <summary>
    /// 体积
    /// </summary>
    public double? Volume { get; set; }

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
    public bool? IsSkinTest { get; set; }

    /// <summary>
    /// 复方药
    /// </summary>
    public bool? IsCompound { get; set; }

    /// <summary>
    /// 麻醉药
    /// </summary>
    public bool? IsDrunk { get; set; }

    /// <summary>
    /// 精神药  0 普通,1 毒,2 麻，3 精神
    /// </summary>
    public int? ToxicLevel { get; set; }

    /// <summary>
    /// 高危药
    /// </summary>
    public bool? IsHighRisk { get; set; }

    /// <summary>
    /// 冷藏药
    /// </summary>
    public bool? IsRefrigerated { get; set; }

    /// <summary>
    /// 肿瘤药
    /// </summary>
    public bool? IsTumour { get; set; }

    /// <summary>
    /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
    /// </summary>
    public int? AntibioticLevel { get; set; }

    /// <summary>
    /// 贵重药
    /// </summary>
    public bool? IsPrecious { get; set; }

    /// <summary>
    /// 胰岛素
    /// </summary>
    public bool? IsInsulin { get; set; }

    /// <summary>
    /// 兴奋剂
    /// </summary>
    public bool? IsAnaleptic { get; set; }

    /// <summary>
    /// 试敏药
    /// </summary>
    public bool? IsAllergyTest { get; set; }

    /// <summary>
    /// 限制性用药标识
    /// </summary>
    public bool? IsLimited { get; set; }

    /// <summary>
    /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
    /// </summary> 
    public int LimitType
    {
        get
        {
            if (IsLimited.HasValue)
            {
                return IsLimited.Value ? 1 : 2;
            }
            return 0;
        }
    }

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
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 急救药
    /// </summary>
    public bool? IsFirstAid { get; set; }

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
    public int AntibioticPermission { get; set; }

    /// <summary>
    /// 处方权
    /// </summary>
    public int PrescriptionPermission { get; set; }


    /// <summary>
    /// 基药标准  N普通,Y国基,P省基,C市基
    /// </summary>
    public string BaseFlag { get; set; }

    /// <summary>
    /// 平台标识
    /// </summary>
    public PlatformType PlatformType { get; set; }

    /// <summary>
    /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
    /// </summary>
    public MedicineUnPack Unpack { get; set; }

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
    /// 排序号
    /// </summary>
    public int IndexNo { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    public string MedicalInsuranceCode { get; set; }


    /// <summary>
    /// 医保统一名称
    /// </summary>
    public string MeducalInsuranceName { get; set; }


    /// <summary>
    /// 医保二级编码
    /// </summary>
    public string YBInneCode { get; set; }

    ///// <summary>
    ///// （急诊处方标志）1.急诊 0.普通 
    ///// </summary>
    public int EmergencySign { get; set; }

    /// <summary>
    /// HIS的药品id
    /// </summary>
    public int MedicineId
    {
        get
        {
            return Id;
        }
    }

    /// <summary>
    /// 重写比较方法
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <returns></returns>
    public bool Equals(MedicineData x, MedicineData y)
    {
        return x.Code == y.Code && x.FactoryCode == y.FactoryCode && x.Specification == y.Specification;
    }

    /// <summary>
    /// 重写HashCode
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public int GetHashCode([DisallowNull] MedicineData obj)
    {
        return obj.Code.GetHashCode() ^ obj.FactoryCode.GetHashCode() ^ obj.Specification.GetHashCode();
    }
}