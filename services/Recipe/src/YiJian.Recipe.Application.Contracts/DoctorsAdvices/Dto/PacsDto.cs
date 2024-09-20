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
    /// 检查项
    /// </summary>
    public class PacsDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 检查项代码
        /// </summary> 
        [Required(ErrorMessage = "检查项代码必填"), StringLength(20, ErrorMessage = "检查项代码长度需在20个字符内")]
        public string Code { get; set; }

        /// <summary>
        /// 检查项名称
        /// </summary> 
        [Required(ErrorMessage = "检查项名称必填"), StringLength(200, ErrorMessage = "检查项名称长度需在200个字符内")]
        public string Name { get; set; }

        /// <summary>
        /// 检查类别编码
        /// </summary> 
        [Required, StringLength(20)]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 检查类别
        /// </summary> 
        [Required, StringLength(100)]
        public string CatalogName { get; set; }
        /// <summary>
        /// 一级检查类别编码
        /// </summary> 
        [StringLength(20)]
        public string FirstCatalogCode { get; set; }

        /// <summary>
        /// 一级检查类别名称
        /// </summary> 
        [StringLength(100)]
        public string FirstCatalogName { get; set; }

        /// <summary>
        /// 临床症状
        /// </summary> 
        [StringLength(2000)]
        public string ClinicalSymptom { get; set; }

        /// <summary>
        /// 病史简要
        /// </summary> 
        [StringLength(2000)]
        public string MedicalHistory { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary> 
        [StringLength(20)]
        public string PartCode { get; set; }

        /// <summary>
        /// 检查部位
        /// </summary> 
        [StringLength(50)]
        public string PartName { get; set; }

        /// <summary>
        /// 分类类型名称 例如心电图申请单、超声申请单
        /// </summary> 
        [StringLength(100)]
        public string CatalogDisplayName { get; set; }

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
        public bool HasReport { get; set; }

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
        /// 检查单单名标题
        /// </summary>
        public string ExamTitle { get; set; }

        /// <summary>
        /// 预约地点
        /// </summary>
        public string ReservationPlace { get; set; }

        /// <summary>
        /// 打印模板Id
        /// </summary>
        public string TemplateId { get; set; }

        /// <summary>
        /// 附加药品编码(多个用','分隔)
        /// </summary>
        public string PrescribeCode { get; set; }

        /// <summary>
        /// 附加处置编码(多个用','分隔)
        /// </summary>
        public string TreatCode { get; set; } = string.Empty;

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
        [StringLength(20)]
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
        /// 检查小项集合
        /// </summary>
        public virtual List<PacsItemDto> Items { get; set; } = new List<PacsItemDto>();

        /// <summary>
        /// 检查病理小项
        /// </summary>
        public PacsPathologyItemDto PacsPathologyItemDto { get; set; }

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
        /// 获取试算价格
        /// </summary> 
        /// <returns></returns>
        public decimal GetAmount(bool isChildren)
        {

            if (isChildren)
            {
                //isAdditionalPrice = true;
                //if (Price > 0) return Price * RecieveQty;
                return Items.Select(c => new
                {
                    Price = c.OtherPrice > 0 ? c.Price = c.OtherPrice : c.Price,
                    c.Qty
                }).Sum(s => s.Price * s.Qty) * RecieveQty;
            }

            //isAdditionalPrice = false;
            if (Price > 0) return Price * RecieveQty;
            return Items.Sum(s => s.Price * s.Qty) * RecieveQty;
        }

        /// <summary>
        /// 附加卡片类型  
        /// 12.TCT细胞学检查申请单 
        /// 11.病理检验申请单 
        /// 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用 
        /// </summary>
        [StringLength(50)]
        public string AddCard { get; set; }

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
            StartTime = model.StartTime;
            EndTime = model.EndTime;
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
