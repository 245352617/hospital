using System;
using System.ComponentModel.DataAnnotations;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 医嘱子项内容
    /// </summary>
    public class ModifySubItemDto
    {
        /// <summary>
        /// 医嘱子项内容
        /// </summary>
        private ModifySubItemDto()
        {

        }

        /// <summary>
        /// 医嘱子项内容
        /// </summary> 
        public ModifySubItemDto(
            string code,
            string name,
            string unit,
            decimal price,
            decimal amount,
            string insuranceCode,
            EInsuranceCatalog insuranceType,
            string payTypeCode,
            ERecipePayType payType,
            string prescriptionNo,
            string recipeNo,
            int recipeGroupNo,
            DateTime? applyTime,
            string recipeCategoryCode,
            string recipeCategoryName,
            bool isBackTracking,
            bool isRecipePrinted,
            string hisOrderNo,
            string positionCode,
            string positionName,
            string execDeptCode,
            string execDeptName,
            string remarks,
            string chargeCode,
            string chargeName,
            string prescribeTypeCode,
            string prescribeTypeName,
            DateTime? startTime,
            DateTime? endTime,
            decimal recieveQty,
            string recieveUnit,
            string pyCode,
            string wbCode,
            bool isAdditionalPrice = false)
        {
            Code = code;
            Name = name;
            Unit = unit;
            Price = price;
            Amount = amount;
            InsuranceCode = insuranceCode;
            InsuranceType = insuranceType;
            PayTypeCode = payTypeCode;
            PayType = payType;
            PrescriptionNo = prescriptionNo;
            RecipeGroupNo = recipeGroupNo;
            RecipeNo = recipeNo;
            ApplyTime = applyTime;
            RecipeCategoryCode = recipeCategoryCode;
            RecipeCategoryName = recipeCategoryName;
            IsBackTracking = isBackTracking;
            IsRecipePrinted = isRecipePrinted;
            HisOrderNo = hisOrderNo;
            PositionCode = positionCode;
            PositionName = positionName;
            ExecDeptCode = execDeptCode;
            ExecDeptName = execDeptName;
            Remarks = remarks;
            ChargeCode = chargeCode;
            ChargeName = chargeName;
            PrescribeTypeCode = prescribeTypeCode;
            PrescribeTypeName = prescribeTypeName;
            StartTime = startTime;
            EndTime = endTime;
            RecieveQty = recieveQty;
            RecieveUnit = recieveUnit;
            PyCode = pyCode;
            WbCode = wbCode;
            IsAdditionalPrice = isAdditionalPrice;
        }

        /// <summary>
        /// 开方编码
        /// </summary> 
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 开方名称
        /// </summary> 
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 总价
        /// </summary>
        public decimal Amount { get; set; }

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
        [Required, StringLength(20)]
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
        [Required, StringLength(20)]
        public string RecipeCategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        [Required, StringLength(20)]
        public string RecipeCategoryName { get; set; }

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
        /// 备注
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
        /// 医嘱类型编码
        /// </summary> 
        [Required, StringLength(20)]
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        [Required, StringLength(20)]
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
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 是否是加收价格
        /// </summary>
        public bool IsAdditionalPrice { get; set; }

        #region 医嘱扩展

        /// <summary>
        /// 拼音码
        /// </summary>  
        public string PyCode { get; set; }

        /// <summary>
        /// 五笔
        /// </summary>  
        public string WbCode { get; set; }

        #endregion

    }
}
