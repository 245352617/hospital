using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace HisApiMockService.Models;
[Table("RegisterPatient")]
public class PatientDto
{
    [Key]
    public int Id { get; set; }
    /// <summary>
    /// 患者病历号
    /// </summary>
    [StringLength(20)]
    public string PatientId { get; set; }

    /// <summary>
    ///诊疗卡号
    /// </summary>
    [StringLength(20)]
    public string? IdCard { get; set; }

    /// <summary>
    ///住院号
    /// </summary>
    [StringLength(20)]
    public string? PatientNo { get; set; }

    /// <summary>
    ///身份证号
    /// </summary>
    [StringLength(20)]
    public string? IdentifyNO { get; set; }

    /// <summary>
    ///患者姓名
    /// </summary>
    [StringLength(50)]
    public string? PatientName { get; set; }

    /// <summary>
    ///出生日期
    /// </summary>
    [StringLength(20)]
    public string? Birthday { get; set; }

    /// <summary>
    ///性别
    /// </summary>
    [StringLength(20)]
    public string? Sex { get; set; }

    /// <summary>
    ///家庭地址
    /// </summary>
    [StringLength(50)]
    public string? HomeAddress { get; set; }

    /// <summary>
    ///工作地址
    /// </summary>
    [StringLength(50)]
    public string? OfficeAddress { get; set; }

    /// <summary>
    ///户口地址
    /// </summary>
    [StringLength(50)]
    public string? Nationaddress { get; set; }

    /// <summary>
    ///家庭电话号码
    /// </summary>
    [StringLength(20)]
    public string? PhoneNumberHome { get; set; }

    /// <summary>
    ///工作电话号码
    /// </summary>
    [StringLength(20)]
    public string? PhoneNumberBus { get; set; }

    /// <summary>
    ///婚姻状态
    /// </summary>
    [StringLength(20)]
    public string? MaritalStatus { get; set; }

    /// <summary>
    ///患者社会保险号
    /// </summary>
    [StringLength(20)]
    public string? SsnNum { get; set; }

    /// <summary>
    ///民族
    /// </summary>
    [StringLength(20)]
    public string? EthnicGroup { get; set; }

    /// <summary>
    ///国籍
    /// </summary>
    [StringLength(20)]
    public string? Nationality { get; set; }

    /// <summary>
    ///患者类别
    /// </summary>
    [StringLength(20)]
    public string? PatientClass { get; set; }

    /// <summary>
    ///就诊号码
    /// </summary>
    [StringLength(20)]
    public string? VisitNum { get; set; }

    /// <summary>
    ///社保流水号
    /// </summary>
    [StringLength(20)]
    public string? AlternateVisitId { get; set; }

    /// <summary>
    ///预约流水号
    /// </summary>
    [StringLength(20)]
    public string? AppointmentId { get; set; }

    /// <summary>
    /// 工作信息
    /// </summary>
    [StringLength(20)]
    public string? Job { get; set; }

    /// <summary>
    /// 体重
    /// </summary>
    [StringLength(20)]
    public string? Weight { get; set; }

    /// <summary>
    /// 联系人姓名
    /// </summary>
    [StringLength(50)]
    public string? ContactName { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    [StringLength(20)]
    public string? ContactPhone { get; set; }

    /// <summary>
    /// 卡类型
    /// </summary>
    [StringLength(20)]
    public string? CardType { get; set; }

    /// <summary>
    /// 卡号
    /// </summary>
    [StringLength(20)]
    public string? CardNo { get; set; }

    /// <summary>
    /// 看诊日期
    /// </summary>
    [StringLength(20)]
    public string? SeeDate { get; set; }

    /// <summary>
    /// 挂号标识
    /// </summary>
    [StringLength(5)]
    public string? RegisterId { get; set; }

    /// <summary>
    /// 挂号顺序号
    /// </summary>
    [StringLength(5)]
    public string? RegisterSequence { get; set; }

    /// <summary>
    /// 挂号时间
    /// </summary>
    [StringLength(20)]
    public string? RegisterDate { get; set; }

    /// <summary>
    /// 号别
    /// </summary>
    [StringLength(5)]
    public string? Shift { get; set; }

    /// <summary>
    /// 科室编码
    /// </summary>
    [StringLength(20)]
    public string? DeptId { get; set; }

    /// <summary>
    /// 操作员
    /// </summary>
    [StringLength(20)]
    public string? Operator { get; set; }

    /// <summary>
    /// 再次就诊标识（就诊次数）
    /// </summary>
    public string? VisitNo { get; set; }

    /// <summary>
    /// 挂号医生编码
    /// </summary>
    public string? DoctorCode { get; set; }

    /// <summary>
    /// 是否已取消挂号
    /// 0未取消；2已取消
    /// </summary>
    public string? IsCancel { get; set; }
}
