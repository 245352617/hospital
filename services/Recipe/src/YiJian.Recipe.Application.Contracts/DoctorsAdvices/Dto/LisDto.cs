using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 检验项
    /// </summary>
    public class LisDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 检验项代码
        /// </summary> 
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 检验项名称
        /// </summary> 
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// 检验类别编码
        /// </summary> 
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检验类别
        /// </summary> 
        [Required, StringLength(100)]
        public string CatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary> 
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 标本编码
        /// </summary> 
        [Required, StringLength(50)]
        public string SpecimenCode { get; set; }

        /// <summary>
        /// 标本名称
        /// </summary> 
        [Required, StringLength(100)]
        public string SpecimenName { get; set; }

        /// <summary>
        /// 标本采集部位编码
        /// </summary> 
        [StringLength(20)]
        public string SpecimenPartCode { get; set; }

        /// <summary>
        /// 标本采集部位
        /// </summary> 
        [StringLength(20)]
        public string SpecimenPartName { get; set; }

        /// <summary>
        /// 标本容器代码
        /// </summary> 
        [StringLength(20)]
        public string ContainerCode { get; set; }

        /// <summary>
        /// 标本容器
        /// </summary> 
        [StringLength(20)]
        public string ContainerName { get; set; }

        /// <summary>
        /// 标本容器颜色:0=红帽,1=蓝帽,2=紫帽
        /// </summary> 
        [StringLength(20)]
        public string ContainerColor { get; set; }

        /// <summary>
        /// 标本说明
        /// </summary> 
        [StringLength(200)]
        public string SpecimenDescription { get; set; }

        /// <summary>
        /// 标本采集时间
        /// </summary> 
        public DateTime? SpecimenCollectDatetime { get; set; }

        /// <summary>
        /// 标本接收时间
        /// </summary> 
        public DateTime? SpecimenReceivedDatetime { get; set; }

        /// <summary>
        /// 出报告时间
        /// </summary> 
        public DateTime? ReportTime { get; set; }

        /// <summary>
        /// 确认报告医生编码
        /// </summary> 
        [StringLength(20)]
        public string ReportDoctorCode { get; set; }

        /// <summary>
        /// 确认报告医生
        /// </summary> 
        [StringLength(50)]
        public string ReportDoctorName { get; set; }

        /// <summary>
        /// 报告标识
        /// </summary> 
        public bool HasReportName { get; set; }

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
        /// 是否紧急
        /// </summary> 
        public bool IsEmergency { get; set; }

        /// <summary>
        /// 是否在床旁
        /// </summary> 
        public bool IsBedSide { get; set; }

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
        /// 指引ID 关联 ExamNote表code
        /// </summary>
        [StringLength(50)]
        public string GuideCode { get; set; }

        /// <summary>
        /// 指引名称 关联 ExamNote表code
        /// </summary>
        [StringLength(2000)]
        public string GuideName { get; set; }

        /// <summary>
        /// 指引单大类
        /// </summary>
        public string GuideCatelogName { get; set; }

        private decimal _recieveQty;

        /// <summary>
        /// 领量(数量)
        /// </summary>
        public decimal RecieveQty
        {
            get
            {
                if (_recieveQty < 1) return 1;
                return _recieveQty;
            }
            set
            {
                if (value < 1) value = 1;
                _recieveQty = value;
            }
        }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }


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
        /// 附加卡片类型  
        /// 15.孕母血清胎儿唐氏综合征筛查申请单(早、中期)
        /// 14.新型冠状病毒RNA检测申请单
        /// 13.基于孕妇外周血胎儿 基于孕妇外周血胎儿13 、18 、21-三体综合征基因筛查申请单
        /// </summary>
        public string AddCard { get; set; }

        /// <summary>
        /// 附加药品编码(多个用','分隔)
        /// </summary>
        public string PrescribeCode { get; set; }

        /// <summary>
        /// 附加处置编码(多个用','分隔)
        /// </summary>
        public string TreatCode { get; set; } = string.Empty;

        /// <summary>
        /// 检验小项集合
        /// </summary>
        public virtual List<LisItemDto> Items { get; set; } = new List<LisItemDto>();

        /// <summary>
        /// 获取试算价格,如果==0 则为返回价格，否则需要查询子项价格*数量 的 总和
        /// </summary> 
        /// <returns></returns>
        public decimal GetAmount()
        {
            if (Price > 0) return Price * RecieveQty;
            return Items.Sum(s => s.Price * s.Qty) * RecieveQty;
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
            PyCode = model.PyCode;
            WbCode = model.WbCode;
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

    }

}
