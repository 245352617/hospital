using System;

namespace HisApiMockService.Models;

/// <summary>
/// 推送【预约确认】接口数据到 HIS
/// </summary>
public class ReservationConfirmDto
{
    /// <summary>
    /// 科室编码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 医生编码
    /// </summary>
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生姓名
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// 绿通标识（1-是 0-否）
    /// </summary>
    public string GreenLogo { get; set; }

    /// <summary>
    /// 患者 ID
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    /// 分诊级别（1-急危 2-急重 3-急症 4a-亚急症 4b-非急症）
    /// </summary>
    public string TriageLevel { get; set; }

    /// <summary>
    /// 日期
    /// </summary>
    public DateTime WorkDate { get; set; }

    /// <summary>
    /// 班次
    /// 金湾（1-上午班 2-下午班 4-全天班）
    /// </summary>
    public string WorkType { get; set; }
}
