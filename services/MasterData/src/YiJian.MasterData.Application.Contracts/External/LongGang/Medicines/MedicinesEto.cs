using Volo.Abp.EventBus;

namespace YiJian.MasterData.External.LongGang.Medicines;

/// <summary>
/// 查询His药品信息出参
/// </summary>
[EventName("DrugEvents")]
public class MedicinesEto
{
    /// <summary>
    /// 医院药品唯一编码
    /// </summary>
    public string DrugCode { get; set; }

    /// <summary>
    /// 商品名称
    /// </summary>
    public string TradeName { get; set; }

    /// <summary>
    /// 学名
    /// </summary>
    public string FormalName { get; set; }

    /// <summary>
    /// 别名
    /// </summary>
    public string OtherName { get; set; }

    /// <summary>
    /// 零售价
    /// </summary>
    public string RetailPrice { get; set; }

    /// <summary>
    /// 最新购入价
    /// </summary>
    public string PurchasePrice { get; set; }

    /// <summary>
    /// 药品规格
    /// </summary>
    public string Specs { get; set; }

    /// <summary>
    /// 药品单位
    /// </summary>
    public string DrugUnit { get; set; }

    /// <summary>
    /// 药品类型  0 西药 1 中成药 2 中草药
    /// </summary>
    public string DrugType { get; set; }

    /// <summary>
    ///  剂型 2.2 药品剂型
    /// </summary>
    public string DoseModelCode { get; set; }

    /// <summary>
    /// 药品剂量
    /// </summary>
    public string BaseDose { get; set; }

    /// <summary>
    /// 一次剂量
    /// </summary>
    public string OnceDose { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public string DoseUnit { get; set; }


    /// <summary>
    /// 最大剂量
    /// </summary>
    public string MaximumDose { get; set; }

    /// <summary>
    /// 皮试标志		0.非皮试药品  1.皮试药品
    /// </summary>
    public string SkinTestSign { get; set; }

    /// <summary>
    /// 药房规格
    /// </summary>
    public string PharmacySpec { get; set; }

    /// <summary>
    /// 药房包装
    /// </summary>
    public string PharmacyPacking { get; set; }

    /// <summary>
    /// 最小单位
    /// </summary>
    public string MinUnit { get; set; }

    /// <summary>
    /// 最小包装
    /// </summary>
    public int MinimumPacking { get; set; }

    /// <summary>
    /// 发药方式
    /// </summary>
    public string DispensingMethod { get; set; }

    /// <summary>
    /// 药品属性
    /// </summary>
    public string DrugAttributes { get; set; }

    /// <summary>
    /// 给药途径
    /// </summary>
    public string DrugChannel { get; set; }

    /// <summary>
    /// 抗生素等级
    /// </summary>
    public string AntibioticGrade { get; set; }

    /// <summary>
    /// 限制用药标志 1.限制用药  2.其他：非限制用药
    /// </summary>
    public string LimitType { get; set; }

    /// <summary>
    /// 限制用药说明
    /// </summary>
    public string LimitData { get; set; }

    /// <summary>
    /// 处方标志	1.处方药品  0.非处方药品
    /// </summary>
    public string PrescriptionMark { get; set; }


    /// <summary>
    /// 拼音码
    /// </summary>
    public string SpellCode { get; set; }

    /// <summary>
    /// 用法编码
    /// </summary>
    public string UsageCode { get; set; }

    /// <summary>
    /// 频次编码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 抗生素标志
    /// </summary>
    public string AntibioticSign { get; set; }

    /// <summary>
    /// 是否需要试敏 0不需要1需要
    /// </summary>
    public string TestFlag { get; set; }

    /// <summary>
    /// 生产厂家
    /// </summary>
    public string ProducerCode { get; set; }
    /// <summary>
    /// 生产厂家名称
    /// </summary>
    public string ProducerName { get; set; }

    /// <summary>
    /// 基药标准
    /// </summary>
    public string Extend2 { get; set; }

    /// <summary>
    /// 包装单位
    /// </summary>
    public string PackUnit { get; set; }

    /// <summary>
    /// 包装类型 1 表示小包装，0表示大包装
    /// </summary>
    public string MinPackageIndicator { get; set; }

    /// <summary>
    /// 药房编码
    /// </summary>
    public string Storage { get; set; }

    /// <summary>
    /// 包装数
    /// </summary>
    public string PackQty { get; set; }

    /// <summary>
    /// 1.作废  0.使用中
    /// </summary>
    public string UseFlag { get; set; }

    /// <summary>
    /// 处方权
    /// </summary>
    public int? OctFlag { get; set; }

    //0：每次发药数量取整
    //1：每天发药数量取整
    //2：发药时不取整
    public string DrugRule { get; set; }

    /// <summary>
    /// 医保类型
    /// </summary>
    public string InsuranceType { get; set; }
    /// <summary>
    /// 1.甲类  2.乙类 3.丙类 4.自费5.少儿(此处为编码)
    /// </summary>
    public string InsuranceCode { get; set; }
    public string BaseFlag { get; set; }

    /// <summary>
    /// 医保编码
    /// </summary>
    public string MedicalInsuranceCode { get; set; }
}
