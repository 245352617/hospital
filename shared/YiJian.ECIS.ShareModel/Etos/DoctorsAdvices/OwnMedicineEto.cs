using Microsoft.EntityFrameworkCore;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// 描述：自备药Eto
/// 创建人： yangkai
/// 创建时间：2022/10/27 16:50:22
/// </summary>
public class OwnMedicineEto
{
    /// <summary>
    /// 前端定位索引，自己录入自己定位
    /// </summary>
    [Comment("前端定位索引，自己录入自己定位")]
    public int Index { get; set; }

    /// <summary>
    /// 唯一标识
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 系统标识: 0=急诊，1=院前
    /// </summary>
    public EPlatformType PlatformType { get; set; }

    /// <summary>
    /// 患者唯一标识
    /// </summary>
    public Guid PIID { get; set; }

    /// <summary>
    /// 患者Id
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 患者名称
    /// </summary>
    public string PatientName { get; set; }

    /// <summary>
    /// 医嘱编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 医嘱名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 开嘱时间
    /// </summary>
    public DateTime ApplyTime { get; set; }

    /// <summary>
    /// 申请医生编码
    /// </summary>
    public string ApplyDoctorCode { get; set; }

    /// <summary>
    /// 申请医生
    /// </summary>
    public string ApplyDoctorName { get; set; }

    /// <summary>
    /// 申请科室编码
    /// </summary>
    public string ApplyDeptCode { get; set; }

    /// <summary>
    /// 申请科室
    /// </summary>
    public string ApplyDeptName { get; set; }

    /// <summary>
    /// 领量(数量)
    /// </summary>
    public decimal RecieveQty { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary>
    public string RecieveUnit { get; set; }


    /// <summary>
    /// 用法编码
    /// </summary>
    public string UsageCode { get; set; }

    /// <summary>
    /// 用法名称
    /// </summary>
    public string UsageName { get; set; }

    /// <summary>
    /// 每次剂量
    /// </summary>
    public decimal DosageQty { get; set; }

    /// <summary>
    /// 剂量单位
    /// </summary>
    public string DosageUnit { get; set; }

    /// <summary>
    /// 频次码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次
    /// </summary>
    public string FrequencyName { get; set; }

    /// <summary>
    /// 在一个周期内执行的次数
    /// </summary>
    public int? FrequencyTimes { get; set; }

    /// <summary>
    /// 医嘱说明
    /// </summary>
    public string Remarks { get; set; }
}
