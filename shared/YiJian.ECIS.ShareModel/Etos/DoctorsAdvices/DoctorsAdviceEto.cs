using System.ComponentModel.DataAnnotations;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 医嘱ETO
/// </summary>
public class DoctorsAdviceEto
{
    /// <summary>
    /// HIS医嘱号
    /// </summary> 
    public string HisOrderNo { get; set; }

    /// <summary>
    /// 医嘱各项分类：0=药方项Prescribe、1=检查项Pacs、2=检验项Lis、3=诊疗项Treat
    /// </summary> 
    public int ItemType { get; set; }

    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 系统标识：0=急诊、1=院前急救
    /// </summary> 
    public int PlatformType { get; set; }

    /// <summary>
    /// 患者入科流水号
    /// </summary> 
    public Guid PIID { get; set; }

    /// <summary>
    /// 患者Id
    /// </summary> 
    [Required, StringLength(20)]
    public string PatientId { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary> 
    public string PatientName { get; set; }

    /// <summary>
    /// 医嘱项目编码
    /// </summary> 
    public string Code { get; set; }

    /// <summary>
    /// 医嘱项目名称
    /// </summary> 
    public string Name { get; set; }

    /// <summary>
    ///  医嘱项目分类编码
    /// </summary> 
    public string CategoryCode { get; set; }

    /// <summary>
    /// 医嘱项目分类 (药物、检查、检验、治疗、护理、膳食、麻醉、手术、会诊、耗材、嘱托、其他)
    /// </summary> 
    public string CategoryName { get; set; }

    /// <summary>
    /// 是否补录
    /// </summary> 
    public bool? IsBackTracking { get; set; }

    /// <summary>
    /// 处方号
    /// </summary> 
    public string PrescriptionNo { get; set; }

    /// <summary>
    /// 医嘱号
    /// </summary>
    public string RecipeNo { get; set; }

    /// <summary>
    /// 医嘱子号
    /// </summary> 
    public int RecipeGroupNo { get; set; }

    /// <summary>
    /// 开嘱时间
    /// </summary> 
    public DateTime ApplyTime { get; set; }

    /// <summary>
    /// 开嘱医生编码
    /// </summary> 
    public string ApplyDoctorCode { get; set; }

    /// <summary>
    /// 开嘱医生名称
    /// </summary> 
    public string ApplyDoctorName { get; set; }

    /// <summary>
    /// 开嘱科室编码
    /// </summary> 
    public string ApplyDeptCode { get; set; }

    /// <summary>
    /// 开嘱科室名称
    /// </summary> 
    public string ApplyDeptName { get; set; }

    /// <summary>
    /// 管培生代码
    /// </summary> 
    public string TraineeCode { get; set; }

    /// <summary>
    /// 管培生名称
    /// </summary> 
    public string TraineeName { get; set; }

    /// <summary>
    /// 执行科室编码
    /// </summary> 
    public string ExecDeptCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary> 
    public string ExecDeptName { get; set; }

    /// <summary>
    /// 医嘱状态：0=未提交、1=已提交、2=已确认、3=已作废、4=已停止、6=已驳回、7=已执行
    /// </summary> 
    public int Status { get; set; } = 1;//开立的时候永远是1

    /// <summary>
    /// 付费类型编码
    /// </summary> 
    public string PayTypeCode { get; set; }

    /// <summary>
    /// 付费类型：0=自费、1=医保、2=其它
    /// </summary> 
    public int? PayType { get; set; }

    /// <summary>
    /// 收费单位
    /// </summary>
    public string Unit { get; set; }

    /// <summary>
    /// 收费单价
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// 总费用
    /// </summary> 
    public decimal Amount { get; set; }

    /// <summary>
    /// 收费类型编码
    /// </summary> 
    public string ChargeCode { get; set; }

    /// <summary>
    /// 收费类型名称
    /// </summary> 
    public string ChargeName { get; set; }

    /// <summary>
    /// 支付状态：0=未支付、1=已支付、2=部分支付、3=已退费
    /// </summary>
    public EPayStatus PayStatus { get; set; }

    /// <summary>
    /// 医保目录编码
    /// </summary> 
    public string InsuranceCode { get; set; }

    /// <summary>
    /// 医保目录：0=自费、1=甲类、2=乙类、3=其它
    /// </summary> 
    public int? InsuranceType { get; set; }

    /// <summary>
    /// 是否慢性病
    /// </summary> 
    public bool? IsChronicDisease { get; set; }

    /// <summary>
    /// 临床诊断
    /// </summary>  
    public string Diagnosis { get; set; }

    /// <summary>
    /// 医嘱说明
    /// </summary> 
    public string Remarks { get; set; }

    /// <summary>
    /// 领量(数量)
    /// </summary> 
    public decimal RecieveQty { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary> 
    public string RecieveUnit { get; set; }

    ///// <summary>
    ///// 执行者编码
    ///// </summary> 
    //public string ExecutorCode { get; set; }

    ///// <summary>
    ///// 执行者名称
    ///// </summary> 
    //public string ExecutorName { get; set; }

    ///// <summary>
    ///// 执行时间
    ///// </summary> 
    //public DateTime? ExecTime { get; set; }

    ///// <summary>
    ///// 是否打印
    ///// </summary> 
    ////public bool IsRecipePrinted { get; set; }

    ///// <summary>
    ///// 停嘱医生代码
    ///// </summary> 
    //public string StopDoctorCode { get; set; }

    ///// <summary>
    ///// 停嘱医生名称
    ///// </summary> 
    //public string StopDoctorName { get; set; }

    ///// <summary>
    ///// 停嘱时间
    ///// </summary> 
    //public DateTime? StopDateTime { get; set; }

    ///// <summary>
    ///// 位置编码
    ///// </summary> 
    //public string PositionCode { get; set; }

    ///// <summary>
    ///// 位置
    ///// </summary> 
    //public string PositionName { get; set; }

    /// <summary>
    /// 开始时间
    /// </summary>
    public DateTime? StartTime { get; set; }

    /// <summary>
    /// 结束时间
    /// </summary> 
    public DateTime? EndTime { get; set; }

    /// <summary>
    /// 医嘱类型编码
    /// </summary> 
    public string PrescribeTypeCode { get; set; }

    /// <summary>
    /// 医嘱类型：临嘱、长嘱、出院带药等
    /// </summary> 
    public string PrescribeTypeName { get; set; }

    /// <summary>
    /// 发票号
    /// </summary>
    public string InvoiceNo { get; set; }

    /// <summary>
    /// 区域编码
    /// </summary>
    public string AreaCode { get; set; }
}
