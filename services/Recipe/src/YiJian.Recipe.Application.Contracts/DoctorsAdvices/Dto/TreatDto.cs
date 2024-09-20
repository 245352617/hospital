using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 诊疗项
    /// </summary>
    public class TreatDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 诊疗项代码
        /// </summary> 
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 检查项名称
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
        /// 其它价格
        /// </summary> 
        public decimal? OtherPrice { get; set; }


        /// <summary>
        /// 加收标志	
        /// </summary> 
        public bool Additional { get; set; }

        /// <summary>
        /// 规格
        /// </summary> 
        [StringLength(200)]
        public string Specification { get; set; }

        /// <summary>
        /// 默认频次代码
        /// </summary> 
        [StringLength(20)]
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次
        /// </summary> 
        [StringLength(20)]
        public string FrequencyName { get; set; }

        /// <summary>
        /// 收费大类代码
        /// </summary> 
        [StringLength(50)]
        public string FeeTypeMainCode { get; set; }

        /// <summary>
        /// 收费小类代码
        /// </summary> 
        [StringLength(50)]
        public string FeeTypeSubCode { get; set; }

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
        [Required, StringLength(20)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        [Required, StringLength(20)]
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
        [StringLength(20)]
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 用法编码
        /// </summary> 
        [StringLength(20)]
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法名称
        /// </summary> 
        [StringLength(20)]
        public string UsageName { get; set; }

        /// <summary>
        /// 开药天数
        /// </summary> 
        public int LongDays { get; set; } = 1;

        /// <summary>
        /// 项目类型
        /// </summary> 
        [StringLength(50)]
        public string ProjectType { get; set; }

        /// <summary>
        /// 项目类型名称
        /// </summary> 
        [StringLength(50)]
        public string ProjectName { get; set; }

        /// <summary>
        /// 项目归类
        /// </summary>
        [StringLength(20)]
        public string ProjectMerge { get; set; }

        /// <summary>
        /// 诊疗项Id
        /// </summary> 
        public int intTreatId { get; set; }

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

        /// <summary>
        /// 附加类型
        /// </summary>
        public EAdditionalItemType AdditionalItemsType { get; set; }
        /// <summary>
        /// 药品附加项id
        /// </summary>
        public Guid AdditionalItemsId { get; set; }
        /// <summary>
        /// 获取试算价格
        /// </summary> 
        /// <returns></returns>
        public decimal GetAmount(bool isChildren, out bool isAdditionalPrice)
        {
            if (isChildren && Additional && OtherPrice.HasValue && OtherPrice.Value > 0)
            {
                isAdditionalPrice = true;
                return OtherPrice.Value * RecieveQty;
            }

            isAdditionalPrice = false;
            return (Price * RecieveQty);
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
            PrescribeTypeCode = model.PrescribeTypeCode;
            PrescribeTypeName = model.PrescribeTypeName;
            Remarks = model.Remarks;
            UsageCode = model.UsageCode;
            UsageName = model.UsageName;

        }

        /// <summary>
        /// 设置拼音，五笔 代码
        /// </summary>
        public void SetPyWbCode()
        {
            if (PyCode.IsNullOrEmpty())
            {
                PyCode = Name.FirstLetterPY();
            }

            if (WbCode.IsNullOrEmpty())
            {
                WbCode = Name.FirstLetterWB();
            }
        }

        public void SetRecieveQty(bool addition, int frequencyTimes, int longDays)
        {
            if (addition) RecieveQty = frequencyTimes * longDays;
        }

        public void SetProject(string projectType, string projectName, string projectMerge)
        {
            ProjectType = projectType;
            ProjectName = projectName;
            ProjectMerge = projectMerge;
        }

    }

}
