using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 开方信息
    /// </summary>
    public class PrescribeDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 药品id （新，重要）
        /// </summary> 
        public int MedicineId { get; set; }

        /// <summary>
        /// 药品编码
        /// </summary> 
        [Required(ErrorMessage = "药品编码是必填项"), StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 药品名称
        /// </summary> 
        [Required(ErrorMessage = "药品名称是必填项"), StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 是否自备药：false=非自备药,true=自备药
        /// </summary> 
        public bool IsOutDrug { get; set; }

        /// <summary>
        /// 药物属性：西药、中药、西药制剂、中药制剂
        /// </summary> 
        [Required(ErrorMessage = "药物属性是必填项"), StringLength(20)]
        public string MedicineProperty { get; set; }

        /// <summary>
        /// 药理等级：如（毒、麻、精一、精二）
        /// eg: ["k1":"v1","k2":"v2","k3":"v3",...]
        /// </summary>  
        public string ToxicProperty { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        [Required(ErrorMessage = "医嘱类型编码是必填项"), StringLength(20)]
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        [Required(ErrorMessage = "医嘱类型名称是必填项"), StringLength(20)]
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary> 
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary> 
        [Required(ErrorMessage = "用法编码是必填项"), StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary> 
        [Required(ErrorMessage = "用法名称是必填项"), StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 滴速
        /// </summary> 
        [StringLength(20)]
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
        /// 剂型
        /// </summary>
        public string DosageForm { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary> 
        [Required(ErrorMessage = "每次剂量是必填项")]
        public decimal DosageQty { get; set; }

        /// <summary>
        /// 每次剂量
        /// </summary>  
        public decimal DefaultDosageQty { get; set; }

        /// <summary>
        /// 每次用量
        /// </summary> 
        public decimal? QtyPerTimes { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        [Required(ErrorMessage = "剂量单位是必填项"), StringLength(20)]
        public string DosageUnit { get; set; }

        /// <summary>
        /// 剂量单位
        /// </summary> 
        [StringLength(20)]
        public string DefaultDosageUnit { get; set; }

        /// <summary>
        /// (Unit)提交给HIS的一次剂量的单位，视图里面的那个Unit单位（原封不动的传过来，不要做任何修改）
        /// </summary> 
        [StringLength(20)]
        public string HisUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary> 
        [StringLength(20)]
        public string HisDosageUnit { get; set; }

        /// <summary>
        /// HIS的一次剂量数量（HIS标准剂量单位的数量）
        /// </summary>  
        public decimal HisDosageQty { get; set; }

        /// <summary>
        /// 提交的一次剂量数量，急诊换算后提交给HIS的一次剂量
        /// </summary> 
        [Required]
        public decimal CommitHisDosageQty { get; private set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        [StringLength(20)]
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 包装规格
        /// </summary> 
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 门诊拆分属性 0最小单位总量取整 1包装单位总量取整 2最小单位每次取整 3包装单位每次取整 4最小单位可拆分
        /// </summary> 
        public EMedicineUnPack Unpack { get; set; }

        /// <summary>
        /// 包装价格
        /// </summary> 
        public decimal BigPackPrice { get; set; }

        /// <summary>
        /// 大包装系数(拆零系数)
        /// </summary> 
        public int BigPackFactor { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary> 
        [StringLength(20)]
        public string BigPackUnit { get; set; }

        /// <summary>
        /// 小包装单价
        /// </summary> 
        public decimal SmallPackPrice { get; set; }

        /// <summary>
        /// 小包装单位
        /// </summary>  
        [StringLength(20)]
        public string SmallPackUnit { get; set; }

        /// <summary>
        /// 小包装系数(拆零系数)
        /// </summary> 
        public int SmallPackFactor { get; set; }

        /// <summary>
        /// 频次码
        /// </summary> 
        [Required(ErrorMessage = "频次编码是必填项"), StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        [Required(ErrorMessage = "频次名称是必填项"), StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 医保编码
        /// </summary> 
        public string MedicalInsuranceCode { get; set; }

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
        /// 一天内的执行时间，格式如："00:01,00:30","21:00:00.000","14:00,02:00","周日：08：00","周一：08：00；周四：08：00；"。
        /// 日时间点只有一个的时候，格式为：HH:mm:ss.fff。
        /// 日时间点多个的时候，格式为：HH:mm，以逗号（,）分割。
        /// 周时间点一个到多个的时候，格式为：周[一 | 二 | 三 | 四 | 五 | 六 | 日 | 天]:HH:mm，以分号（;）分割。 
        /// </summary> 
        [StringLength(500)]
        public string FrequencyExecDayTimes { get; set; }

        /// <summary>
        /// HIS频次编码
        /// </summary>
        [StringLength(20)]
        public string DailyFrequency { get; set; }

        /// <summary>
        /// 药房编码
        /// </summary> 
        [StringLength(20)]
        public string PharmacyCode { get; set; }

        /// <summary>
        /// 药房
        /// </summary> 
        [StringLength(50)]
        public string PharmacyName { get; set; }

        /// <summary>
        /// 厂家
        /// </summary> 
        [StringLength(100)]
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂家代码
        /// </summary> 
        [StringLength(20)]
        public string FactoryCode { get; set; }

        /// <summary>
        /// 批次号
        /// </summary> 
        [StringLength(20)]
        public string BatchNo { get; set; }

        /// <summary>
        /// 失效期
        /// </summary> 
        public DateTime? ExpirDate { get; set; }

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
        /// 耗材金额
        /// </summary>  
        public decimal? MaterialPrice { get; set; }

        /// <summary>
        /// 用于判断关联耗材是否手动删除
        /// </summary> 
        public bool? IsBindingTreat { get; set; }

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

        /// <summary>
        /// 单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 医保目录编码
        /// </summary> 
        [StringLength(20)]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary> 
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary> 
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 处方号
        /// </summary> 
        [StringLength(20)]
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 医嘱号,如果前端传"auto" 过来，我自动生产；如果前端传具体值过来，我保存前端传过来的值
        /// </summary>  
        [StringLength(20)]
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改），提供修改，前端自行操作
        /// </summary> 
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary> 
        [Required(ErrorMessage = "医嘱项目分类编码是必填项"), StringLength(20)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        [Required(ErrorMessage = "医嘱项目分类名称是必填项"), StringLength(20)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary> 
        public bool IsBackTracking { get; set; }

        /// <summary>
        /// 是否打印过
        /// </summary> 
        public bool IsRecipePrinted { get; set; }

        /// <summary>
        /// HIS医嘱号
        /// </summary> 
        [StringLength(20)]
        public string HisOrderNo { get; set; }

        /// <summary>
        /// 位置编码
        /// </summary> 
        [StringLength(20)]
        public string PositionCode { get; set; }

        /// <summary>
        /// 位置
        /// </summary> 
        [StringLength(100)]
        public string PositionName { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary> 
        [StringLength(20)]
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary> 
        [StringLength(50)]
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 抗生素权限
        /// </summary> 
        public int AntibioticPermission { get; set; }

        /// <summary>
        /// 处方权
        /// </summary>
        public int PrescriptionPermission { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary> 
        [StringLength(500)]
        public string Remarks { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary> 
        [StringLength(20)]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary> 
        [StringLength(50)]
        public string ChargeName { get; set; }


        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary> 
        public int LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

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

        #region 医嘱扩展

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
        [StringLength(50)]
        public string WbCode { get; set; }



        #endregion

        /// <summary>
        /// 是否处方权 
        /// </summary>
        public bool IsCriticalPrescription { get; set; }

        /// <summary>
        /// 获取试算价格
        /// </summary>
        /// <returns></returns>
        public decimal GetAmount()
        {
            if (IsOutDrug) return 0;  //自备药：总价=0
            if (RecieveUnit.Trim().ToUpper() == SmallPackUnit.Trim().ToUpper() && SmallPackPrice > 0) return SmallPackPrice * RecieveQty; //可拆包装（前端提供的单位对比）：总价=小包装价格x数量
            if (RecieveUnit.Trim().ToUpper() == BigPackUnit.Trim().ToUpper() && BigPackPrice > 0) return BigPackPrice * RecieveQty; //不拆包装（前端提供的单位对比）：总价 = 大包装价格x数量
            return Price * RecieveQty; //传入的单价*领量
        }



        /// <summary>
        /// 填充数据
        /// </summary> 
        public void FillData(DoctorsAdvicePartialDto model)
        {
            Code = model.Code;
            Name = model.Name;
            PositionCode = model.PositionCode;
            PositionName = model.PositionName;
            Unit = model.Unit;
            Price = model.Price;
            InsuranceCode = model.InsuranceCode;
            InsuranceType = model.InsuranceType;
            PayTypeCode = model.PayTypeCode;
            PayType = model.PayType;
            PrescriptionNo = model.PrescriptionNo;
            RecipeNo = model.RecipeNo;
            RecipeGroupNo = model.RecipeGroupNo;
            ApplyTime = model.ApplyTime.HasValue ? model.ApplyTime.Value : DateTime.Now;
            CategoryCode = model.CategoryCode;
            CategoryName = model.CategoryName;
            IsBackTracking = model.IsBackTracking;
            IsRecipePrinted = model.IsRecipePrinted;
            HisOrderNo = model.HisOrderNo;
            ExecDeptCode = model.ExecDeptCode;
            ExecDeptName = model.ExecDeptName;
            ChargeCode = model.ChargeCode;
            ChargeName = model.ChargeName;
            Remarks = model.Remarks;
            PyCode = model.PyCode;
            WbCode = model.WbCode;
        }


        /// <summary>
        /// 设置医嘱号
        /// </summary>
        /// <param name="recipeNo"></param>
        /// <param name="recipeGroupNo"></param>
        public void SetRecipeNo(string recipeNo, int recipeGroupNo)
        {
            RecipeNo = recipeNo;
            RecipeGroupNo = recipeGroupNo;
        }

        /// <summary>
        /// 设置提交给HIS的一次领量
        /// </summary> 
        /// <returns></returns>
        public void SetHisDosageQty()
        {
            var unit = Unit.Trim();
            //急诊的一次剂量单位和HIS的一次剂量一致
            if (unit == HisDosageUnit)
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
                        if (unit == HisUnit)
                        {
                            CommitHisDosageQty = DosageQty * HisDosageQty;
                        }
                        else if (unit == SmallPackUnit)
                        {
                            CommitHisDosageQty = DosageQty * SmallPackFactor * HisDosageQty;
                        }
                        else if (unit == BigPackUnit)
                        {
                            CommitHisDosageQty = DosageQty * BigPackFactor * HisDosageQty;
                        }
                    }
                    break;
                //Unpack = 2
                case EMedicineUnPack.RoundByMinUnitTime:
                //Unpack = 3
                case EMedicineUnPack.RoundByPackUnitTime:
                    {
                        if (unit == HisUnit)
                        {
                            CommitHisDosageQty = DosageQty * HisDosageQty;
                        }
                        else if (unit == SmallPackUnit)
                        {
                            CommitHisDosageQty = DosageQty * (BigPackFactor * HisDosageQty);
                        }
                        else if (unit == BigPackUnit)
                        {
                            CommitHisDosageQty = DosageQty * (SmallPackFactor * HisDosageQty);
                        }
                    }
                    break;
                default:
                    break;
            }
        }

    }

}
