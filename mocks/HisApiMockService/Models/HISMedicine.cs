using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace HisApiMockService.Models
{
    /// <summary>
    /// 药品字典
    /// </summary> 
    public class HISMedicine  
    {
        #region 属性

        /// <summary>
        /// 药品编码
        /// </summary>
        // [Required]  
        public decimal MedicineCode { get; set; }
         
        /// <summary>
        /// 药品名称
        /// </summary> 
        public string MedicineName { get; set; }

        /// <summary>
        /// 学名
        /// </summary> 
        public string ScientificName { get; set; }

        string _alias;

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
        public decimal MedicineProperty { get; set; }


        #region 剂量

        /// <summary>
        /// 默认剂量
        /// </summary>  
        public decimal DefaultDosage { get; set; }

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

        #endregion 基本(单位)价格

        #region 大包装价格

        /// <summary>
        /// 大包装价格
        /// </summary> 
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装量(大包装系数)
        /// </summary>
        // [Required] 
        public decimal BigPackFactor { get; set; }

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
        public decimal SmallPackFactor { get; set; }

        #endregion 小包装价格

        /// <summary>
        /// 儿童价格
        /// </summary> 
        public int ChildrenPrice { get; set; }

        /// <summary>
        /// 批发价格
        /// </summary> 
        public decimal? FixPrice { get; set; }

        /// <summary>
        /// 零售价格
        /// </summary> 
        public decimal? RetPrice { get; set; }

        #region 医保

        ///// <summary>
        ///// 医保类型：0自费,1甲类,2乙类，3丙类
        ///// </summary>
        //[Comment("医保类型：0自费,1甲类,2乙类，3丙类")]
        //[Obsolete("与InsuranceCode重复属性，且此处不应该使用枚举硬编码")]
        // public InsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 医保类型代码
        /// </summary> 
        public decimal InsuranceCode { get; set; }

        /// <summary>
        /// 医保类型名称
        /// </summary> 
        public string InsuranceName { get; set; }

        /// <summary>
        /// 医保支付比例
        /// </summary> 
        public string InsurancePayRate { get; set; }

        #endregion 医保

        #region 厂家

        /// <summary>
        /// 厂家名称
        /// </summary> 
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary>  
        public decimal FactoryCode { get; set; }

        ///// <summary>
        ///// 批次号
        ///// </summary>
        //[StringLength(20)]
        //[Comment("批次号")]
        //public string BatchNo { get; set; }

        ///// <summary>
        ///// 失效期
        ///// </summary>
        //[StringLength(20)]
        //[Comment("失效期")]
        //public DateTime? ExpirDate { get; set; }

        #endregion 厂家

        #region 重量

        /// <summary>
        /// 重量
        /// </summary> 
        public decimal? Weight { get; set; }

        /// <summary>
        /// 重量单位
        /// </summary> 
        public string WeightUnit { get; set; }

        #endregion 重量

        #region 体积

        /// <summary>
        /// 体积
        /// </summary> 
        public string Volume { get; set; }

        /// <summary>
        /// 体积单位
        /// </summary> 
        public string VolumeUnit { get; set; }

        #endregion 体积

        /// <summary>
        /// 备注
        /// </summary> 
        public string Remark { get; set; }

        /// <summary>
        /// 皮试药
        /// </summary> 
        public decimal? IsSkinTest { get; set; }

        /// <summary>
        /// 复方药
        /// </summary> 
        public string IsCompound { get; set; }

        /// <summary>
        /// 麻醉药
        /// </summary> 
        public int? IsDrunk { get; set; }

        /// <summary>
        /// 精神药  0非精神药,1一类精神药,2二类精神药
        /// </summary> 
        public decimal? ToxicLevel { get; set; }

        /// <summary>
        /// 高危药
        /// </summary> 
        public string IsHighRisk { get; set; }

        /// <summary>
        /// 冷藏药
        /// </summary> 
        public string IsRefrigerated { get; set; }

        /// <summary>
        /// 肿瘤药
        /// </summary> 
        public string IsTumour { get; set; }

        /// <summary>
        /// 抗菌药  0非抗菌药,1非限制级,2限制级,3特殊使用级
        /// </summary> 
        public int AntibioticLevel { get; set; }

        /// <summary>
        /// 贵重药
        /// </summary> 
        public int? IsPrecious { get; set; }

        /// <summary>
        /// 胰岛素
        /// </summary> 
        public int? IsInsulin { get; set; }

        /// <summary>
        /// 兴奋剂
        /// </summary> 
        public int? IsAnaleptic { get; set; }

        /// <summary>
        /// 试敏药
        /// </summary> 
        public int? IsAllergyTest { get; set; }

        /// <summary>
        /// 限制性用药标识
        /// </summary> 
        public string IsLimited { get; set; }

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
        public decimal ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary> 
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 默认用法编码
        /// </summary>  
        public decimal? UsageCode { get; set; }

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
        public int IsActive { get; set; } = 1;

        ///// <summary>
        ///// 急救药
        ///// </summary> 
        public int IsFirstAid { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary> 
        public string MedicalInsuranceCode { get; set; }

        #region 药房

        ///// <summary>
        ///// 药房代码
        ///// </summary> 
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
        public decimal AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary> 
        public decimal PrescriptionPermission { get; set; }

        #endregion 权限

        //// /// <summary>
        //// /// 库存
        //// /// </summary>
        //// [Comment("库存")]
        //// public int Stock { get; set; }

        /// <summary>
        /// 基药标准  01国基，02省基，03市基，04基药，05中草药，06非基药
        /// </summary> 
        public string BaseFlag { get; set; }

        ///// <summary>
        ///// 平台标识
        ///// </summary> 
        public int PlatformType { get; set; }

        ///// <summary>
        ///// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        ///// </summary> 
        public int Unpack { get; set; }

        #endregion 属性


        /// <summary>
        /// 库存
        /// </summary>  
        public decimal? Qty { get; set; }

        ///// <summary>
        ///// 库存记录唯一ID 
        ///// </summary>
        //[StringLength(20)]
        //[Comment("库存记录唯一ID ")] 
        [Key]
        public decimal InvId { get; set; }

        ///// <summary>
        ///// （急诊处方标志）1.急诊 0.普通 
        ///// </summary> 
        public decimal EmergencySign { get; set; }

        /// <summary>
        /// 对比
        /// </summary>
        /// <param name="medicine"></param>
        /// <returns></returns>
        public bool Eq(HISMedicine medicine)
        {
            bool flag = true;
            flag = flag || (MedicineCode == medicine.MedicineCode);
            flag = flag || (ScientificName == medicine.ScientificName);
            flag = flag || (AliasPyCode == medicine.AliasPyCode);
            flag = flag || (AliasWbCode == medicine.AliasWbCode);
            flag = flag || (PyCode == medicine.PyCode);
            flag = flag || (WbCode == medicine.WbCode);
            flag = flag || (MedicineProperty == medicine.MedicineProperty);
            flag = flag || (DefaultDosage == medicine.DefaultDosage);
            flag = flag || (DosageQty == medicine.DosageQty);
            flag = flag || (DosageUnit == medicine.DosageUnit);
            flag = flag || (Unit == medicine.Unit);
            flag = flag || (Price == medicine.Price);
            flag = flag || (BigPackPrice == medicine.BigPackPrice);
            flag = flag || (BigPackFactor == medicine.BigPackFactor);
            flag = flag || (BigPackUnit == medicine.BigPackUnit);
            flag = flag || (SmallPackPrice == medicine.SmallPackPrice);
            flag = flag || (SmallPackUnit == medicine.SmallPackUnit);
            flag = flag || (SmallPackFactor == medicine.SmallPackFactor);
            flag = flag || (ChildrenPrice == medicine.ChildrenPrice);
            flag = flag || (FixPrice == medicine.FixPrice);
            flag = flag || (RetPrice == medicine.RetPrice);
            flag = flag || (InsuranceCode == medicine.InsuranceCode);
            flag = flag || (InsuranceName == medicine.InsuranceName);
            flag = flag || (InsurancePayRate == medicine.InsurancePayRate);
            flag = flag || (FactoryName == medicine.FactoryName);
            flag = flag || (FactoryCode == medicine.FactoryCode);
            flag = flag || (Weight == medicine.Weight);
            flag = flag || (WeightUnit == medicine.WeightUnit);
            flag = flag || (Volume == medicine.Volume);
            flag = flag || (VolumeUnit == medicine.VolumeUnit);
            flag = flag || (Remark == medicine.Remark);
            flag = flag || (IsSkinTest == medicine.IsSkinTest);
            flag = flag || (IsCompound == medicine.IsCompound);
            flag = flag || (IsDrunk == medicine.IsDrunk);
            flag = flag || (ToxicLevel == medicine.ToxicLevel);
            flag = flag || (IsHighRisk == medicine.IsHighRisk);
            flag = flag || (IsRefrigerated == medicine.IsRefrigerated);
            flag = flag || (IsTumour == medicine.IsTumour);
            flag = flag || (AntibioticLevel == medicine.AntibioticLevel);
            flag = flag || (IsPrecious == medicine.IsPrecious);
            flag = flag || (IsInsulin == medicine.IsInsulin);
            flag = flag || (IsAnaleptic == medicine.IsAnaleptic);
            flag = flag || (IsAllergyTest == medicine.IsAllergyTest);
            flag = flag || (IsLimited == medicine.IsLimited);
            flag = flag || (LimitedNote == medicine.LimitedNote);
            flag = flag || (Specification == medicine.Specification);
            flag = flag || (DosageForm == medicine.DosageForm);
            flag = flag || (ExecDeptCode == medicine.ExecDeptCode);
            flag = flag || (ExecDeptName == medicine.ExecDeptName);
            flag = flag || (UsageCode == medicine.UsageCode);
            flag = flag || (UsageName == medicine.UsageName);
            flag = flag || (FrequencyCode == medicine.FrequencyCode);
            flag = flag || (FrequencyName == medicine.FrequencyName);
            flag = flag || (IsActive == medicine.IsActive);
            flag = flag || (IsFirstAid == medicine.IsFirstAid);
            flag = flag || (PharmacyCode == medicine.PharmacyCode);
            flag = flag || (PharmacyName == medicine.PharmacyName);
            flag = flag || (AntibioticPermission == medicine.AntibioticPermission);
            flag = flag || (PrescriptionPermission == medicine.PrescriptionPermission);
            flag = flag || (BaseFlag == medicine.BaseFlag);
            flag = flag || (Unpack == medicine.Unpack);
            flag = flag || (InvId == medicine.InvId);
            flag = flag || (EmergencySign == medicine.EmergencySign);
            flag = flag || (EmergencySign == medicine.EmergencySign);


            return flag;

        }


        public object[] GetKeys()
        {
            return new object[] { InvId };
        }
    }
}
