namespace HisApiMockService.Models;

public class RegisterPatientRequest
{
    /// <summary>
    /// 患者病历号
    /// </summary>
    public string PatientId { get; set; }


    #region 预约信息段

    /// <summary>
    ///日程表ID
    /// </summary>
    public string No { get; set; }

    /// <summary>
    ///科室编码
    /// </summary>
    public string DeptId { get; set; }

    /// <summary>
    ///科室名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    ///医生编码
    /// </summary>
    public string DoctorId { get; set; }

    /// <summary>
    ///看诊日期
    /// </summary>
    public string SeeDate { get; set; }

    /// <summary>
    ///开始时间
    /// </summary>
    public string BeginTime { get; set; }

    /// <summary>
    ///结束时间
    /// </summary>
    public string EndTime { get; set; }

    /// <summary>
    ///挂号级别代码
    /// </summary>
    public string ReglevlCode { get; set; }

    /// <summary>
    ///挂号级别名称
    /// </summary>
    public string ReglevlName { get; set; }

    /// <summary>
    ///午别（1：上午，2：下午，3：晚上）
    /// </summary>
    public string NoonId { get; set; }

    /// <summary>
    ///是否医保（1 是  0 否）
    /// </summary>
    public string Insurance { get; set; }

    #endregion 预约信息段

    #region 取消预挂号

    /// <summary>
    ///患者类别
    /// </summary>
    public string PatientClass { get; set; }

    /// <summary>
    ///就诊流水号
    /// </summary>
    public string VisitNum { get; set; }

    #endregion 取消预挂号

    /// <summary>
    /// 登记窗口
    /// </summary>
    public string SiteCode { get; set; }

    /// <summary>
    /// 操作员  加@防止与保留关键字冲突
    /// </summary>
    public string Operator { get; set; }
}
