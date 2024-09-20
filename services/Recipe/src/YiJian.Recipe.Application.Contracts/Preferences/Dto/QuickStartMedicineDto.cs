using System;
using Volo.Abp.Application.Dtos;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Preferences.Dto
{

    /// <summary>
    /// 快速开嘱的药品信息
    /// </summary>
    public class QuickStartMedicineDto : EntityDto<Guid>
    {
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
        /// 五笔
        /// </summary> 
        public string WbCode { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary> 
        public string MedicineProperty { get; set; }

        #region 剂量

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

        #endregion 剂量

        #region 基本(单位)价格

        /// <summary>
        /// 基本单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 基本单位价格
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary> 
        public decimal Qty { get; set; }

        #endregion 基本(单位)价格

        #region 大包装价格

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

        #endregion 大包装价格

        #region 小包装价格

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

        #endregion 小包装价格

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

        #region 医保

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

        #endregion 医保

        #region 厂家

        /// <summary>
        /// 厂家名称
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
        /// 失效期
        /// </summary> 
        public DateTime? ExpireDate { get; set; }

        #endregion 厂家

        #region 重量

        /// <summary>
        /// 重量
        /// </summary> 
        public double? Weight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary> 
        public string WeightUnit { get; set; }

        #endregion 重量

        #region 体积

        /// <summary>
        /// 体积
        /// </summary> 
        public double? Volume { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary> 
        public string VolumeUnit { get; set; }

        #endregion 体积

        /// <summary>
        /// 备注(医嘱说明)
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
        /// 精神药  0非精神药,1一类精神药,2二类精神药
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
        /// 执行科室名称
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
        /// 是否启用
        /// </summary> 
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// 急救药
        /// </summary> 
        public bool? IsFirstAid { get; set; }

        #region 药房

        /// <summary>
        /// 药房代码
        /// </summary> 
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        public string PharmacyName { get; set; }

        #endregion 药房

        #region 权限

        /// <summary>
        /// 抗生素权限
        /// </summary> 
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary> 
        public int PrescriptionPermission { get; set; }

        #endregion 权限

        /// <summary>
        /// 库存
        /// </summary> 
        public int Stock { get; set; }

        /// <summary>
        /// 基药标准  N普通,Y国基,P省基,C市基
        /// </summary> 
        public string BaseFlag { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary> 
        public MedicineUnPack Unpack { get; set; }

        /// <summary>
        /// 快速开嘱医嘱信息Id
        /// </summary>
        public Guid QuickStartAdviceId { get; set; }
        /// <summary>
        /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
        /// </summary> 
        public string HisUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary> 
        public string HisDosageUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary> 
        public decimal HisDosageQty { get; set; }

        /// <summary>
        /// 提交的一次剂量数量，急诊换算后提交给HIS的一次剂量
        /// </summary> 
        public decimal CommitHisDosageQty { get; private set; }

    }

}
