namespace YiJian.MasterData;

public class DoctorDto
{
    /// <summary>
    /// id
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// 医生代码
    /// </summary>
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生姓名
    /// </summary>
    public string DoctorName { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    public string BranchCode { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    public string BranchName { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    public string DeptName { get; set; }

    /// <summary>
    /// 医生性别
    /// </summary>
    public string Sex { get; set; }

    /// <summary>
    /// 医生职称
    /// </summary>
    public string DoctorTitle { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    public string Telephone { get; set; }

    /// <summary>
    /// 医生擅长
    /// </summary>
    public string Skill { get; set; }

    /// <summary>
    /// 医生简介
    /// </summary>
    public string Introdution { get; set; }

    /// <summary>
    /// 麻醉处方权限
    /// </summary>
    public bool AnaesthesiaAuthority { get; set; }

    /// <summary>
    /// 处方权限
    /// </summary>
    public bool DrugAuthority { get; set; }

    /// <summary>
    /// 精神处方权限
    /// </summary>
    public bool SpiritAuthority { get; set; }

    /// <summary>
    /// 抗生素处方权限
    /// </summary>
    public bool AntibioticAuthority { get; set; }

    /// <summary>
    /// 医师执业代码
    /// </summary>
    public string PracticeCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    public bool IsActive { get; set; }

    /// <summary>
    /// 人员类型		1.急诊医生  2.急诊护士 0.其他人员
    /// </summary>
    public int DoctorType { get; set; }
}