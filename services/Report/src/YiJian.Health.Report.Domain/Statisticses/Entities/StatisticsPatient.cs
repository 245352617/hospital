using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.Health.Report.Statisticses.Entities;

/// <summary>
/// 患者详情
/// </summary>
public class StatisticsPatient
{
    /// <summary>
    /// 序号
    /// </summary>
    [NotMapped]
    public int Row { get; set; }

    /// <summary>
    /// 患者Gid
    /// </summary>
    [Key]
    public Guid PI_ID { get; set; }

    /// <summary>
    /// 就诊流水号
    /// </summary>
    public string VisSerialNo { get; set; }

    /// <summary>
    /// 患者姓名
    /// </summary>
    public string PatientName { get; set; }

    /// <summary>
    /// 性别
    /// </summary>
    public string SexName { get; set; }

    /// <summary>
    /// 年龄
    /// </summary>
    public string Age { get; set; }

    /// <summary>
    /// 出生日期
    /// </summary>
    public DateTime? Birthday { get; set; }

    /// <summary>
    /// 证件号
    /// </summary>
    public string IDNo { get; set; }

    /// <summary>
    /// 联系方式
    /// </summary>
    public string ContactsPhone { get; set; }

    /// <summary>
    /// 接诊医生
    /// </summary>
    public string FirstDoctorName { get; set; }

    /// <summary>
    /// 接诊时间
    /// </summary>
    public DateTime? VisitDate { get; set; }

    /// <summary>
    /// 诊断
    /// </summary>
    public string TriageInfo { get; set; }

}

public class TotalCount
{
    public int Cnt { get; set; }

    [Key]
    public Guid PI_ID { get; set; }
}