using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Volo.Abp.EventBus;

namespace YiJian.MasterData.MasterData.HospitalClient.Doctors;

/// <summary>
/// 医生字典信息
/// </summary>
[EventName("DoctorEvents")]
public class DoctorEto
{
    /// <summary>
    /// 医生代码
    /// </summary>
    [JsonProperty("doctorCode")]
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生姓名
    /// </summary>
    [JsonProperty("doctorName")]
    public string DoctorName { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    [JsonProperty("branchCode")]
    public string BranchCode { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    [JsonProperty("branchName")]
    public string BranchName { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    [JsonProperty("deptCode")]
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [JsonProperty("deptName")]
    public string DeptName { get; set; }

    /// <summary>
    /// 医生性别
    /// </summary>
    [JsonProperty("doctorSex")]
    public string Sex { get; set; }

    /// <summary>
    /// 医生职称
    /// </summary>
    [JsonProperty("doctorTitle")]
    public string DoctorTitle { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [JsonProperty("doctorTelephone")]
    public string Telephone { get; set; }

    /// <summary>
    /// 医生擅长
    /// </summary>
    [JsonProperty("doctorSkill")]
    public string Skill { get; set; }

    /// <summary>
    /// 医生简介
    /// </summary>
    [JsonProperty("doctorIntrodution")]
    public string Introdution { get; set; }

    /// <summary>
    /// 麻醉处方权限
    /// </summary>
    [JsonProperty("anaesthesiaAuthority")]
    public string AnaesthesiaAuthority { get; set; }

    /// <summary>
    /// 处方权限
    /// </summary>
    [JsonProperty("drugAuthority")]
    public string DrugAuthority { get; set; }

    /// <summary>
    /// 精神处方权限
    /// </summary>
    [JsonProperty("spiritAuthority")]
    public string SpiritAuthority { get; set; }

    /// <summary>
    /// 抗生素处方权限
    /// </summary>
    [JsonProperty("antibioticAuthority")]
    public string AntibioticAuthority { get; set; }

    /// <summary>
    /// 医师执业代码
    /// </summary>
    [JsonProperty("practiceCode")]
    public string PracticeCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    [JsonProperty("useFlag")]
    public int IsActive { get; set; }

    /// <summary>
    /// 人员类型		1.急诊医生  2.急诊护士 0.其他人员
    /// </summary>
    [Comment("人员类型	1.急诊医生  2.急诊护士 0.其他人员")]
    [JsonProperty("doctType")]
    public int DoctorType { get; set; }
}