namespace HisApiMockService.Models;

public class CreatePatientDto
{
    /// <summary>
    /// 患者证件类型
    /// </summary>
    public string PatIdType { get; set; }

    /// <summary>
    /// 患者病历号
    /// </summary>
    public string PatientId { get; set; }

    /// <summary>
    ///证件号
    /// </summary>
    public string IdNo { get; set; }

    /// <summary>
    ///证件类型
    /// </summary>
    public string IdType { get; set; }

    /// <summary>
    /// 患者姓名
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    ///性别
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    ///出生日期
    /// </summary>
    public string Birthday { get; set; }

    /// <summary>
    ///手机号码
    /// </summary>
    public string Phone { get; set; }

    /// <summary>
    ///订单号
    /// </summary>
    public string OrderNum { get; set; }

    /// <summary>
    ///订单时间
    /// </summary>
    public string OrderTime { get; set; }

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
    /// 卡类型
    /// </summary>
    public string CardType { get; set; }

    /// <summary>
    /// 卡号
    /// </summary>
    public string CardNo { get; set; }

    /// <summary>
    /// 国籍
    /// </summary>
    public string Nationality { get; set; }

    /// <summary>
    /// 民族
    /// </summary>
    public string EthnicGroup { get; set; }

    /// <summary>
    /// 家庭地址
    /// </summary>
    public string HomeAddress { get; set; }

    /// <summary>
    /// 家庭电话号码
    /// </summary>
    public string PhoneNumberHome { get; set; }

    /// <summary>
    /// 联系人姓名
    /// </summary>
    public string ContactName { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    public string ContactPhone { get; set; }

    /// <summary>
    /// 体重
    /// </summary>
    public string Weight { get; set; }

    /// <summary>
    /// 登记窗口
    /// </summary>
    public string SiteCode { get; set; }

    /// <summary>
    /// 操作员  加@防止与保留关键字冲突
    /// </summary>
    public string Operator { get; set; }

    /// <summary>
    /// 人群编码
    /// </summary>
    public string CrowdCode { get; set; }

    /// <summary>
    /// 联系人姓名
    /// </summary>
    public string AssociationName { get; set; }

    /// <summary>
    /// 联系人证件类型
    /// </summary>
    public string AssociationIdType { get; set; }

    /// <summary>
    /// 联系人证件号码
    /// </summary>
    public string AssociationIdNo { get; set; }

    /// <summary>
    /// 与联系人关系
    /// </summary>
    public string SocietyRelation { get; set; }

    /// <summary>
    /// 联系人电话
    /// </summary>
    public string AssociationPhone { get; set; }

    /// <summary>
    /// 联系人地址
    /// </summary>
    public string AssociationAddress { get; set; }
}
