using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.MasterData.Doctors;

/// <summary>
/// 医生信息
/// </summary>
[Comment("医生信息")]
public class Doctor : FullAuditedAggregateRoot<int>, IIsActive
{
    public void SetId(int id)
    {
        Id = id;
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="doctorName"></param>
    /// <param name="branchCode"></param>
    /// <param name="branchName"></param>
    /// <param name="deptCode"></param>
    /// <param name="deptName"></param>
    /// <param name="sex"></param>
    /// <param name="doctorTitle"></param>
    /// <param name="telephone"></param>
    /// <param name="skill"></param>
    /// <param name="introdution"></param>
    /// <param name="anaesthesiaAuthority"></param>
    /// <param name="drugAuthority"></param>
    /// <param name="spiritAuthority"></param>
    /// <param name="antibioticAuthority"></param>
    /// <param name="practiceCode"></param>
    /// <param name="isActive"></param>
    /// <param name="doctorType"></param>
    public void Update(string doctorName, string branchCode, string branchName, string deptCode, string deptName,
        string sex, string doctorTitle, string telephone, string skill, string introdution,
        bool anaesthesiaAuthority, bool drugAuthority, bool spiritAuthority, bool antibioticAuthority,
        string practiceCode, bool isActive, int doctorType)
    {
        DoctorName = doctorName;
        BranchCode = branchCode;
        BranchName = branchName;
        DeptCode = deptCode;
        DeptName = deptName;
        Sex = sex;
        DoctorTitle = doctorTitle;
        Telephone = telephone;
        Skill = skill;
        Introdution = introdution;
        AnaesthesiaAuthority = anaesthesiaAuthority;
        DrugAuthority = drugAuthority;
        SpiritAuthority = spiritAuthority;
        AntibioticAuthority = antibioticAuthority;
        PracticeCode = practiceCode;
        DoctorType = doctorType;
        SetActive(isActive);
    }

    /// <summary>
    /// 是否启用
    /// </summary>
    /// <param name="isActive"></param>
    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }
    /// <summary>
    /// 医生代码
    /// </summary>
    [StringLength(20), Required]
    [Comment("医生代码")]
    public string DoctorCode { get; set; }

    /// <summary>
    /// 医生姓名
    /// </summary>
    [StringLength(50), Required]
    [Comment("医生姓名")]
    public string DoctorName { get; set; }

    /// <summary>
    /// 机构编码
    /// </summary>
    [StringLength(20), Required]
    [Comment("机构编码")]
    public string BranchCode { get; set; }

    /// <summary>
    /// 机构名称
    /// </summary>
    [StringLength(50), Required]
    [Comment("机构名称")]
    public string BranchName { get; set; }

    /// <summary>
    /// 科室代码
    /// </summary>
    [StringLength(20), Required]
    [Comment("科室代码")]
    public string DeptCode { get; set; }

    /// <summary>
    /// 科室名称
    /// </summary>
    [StringLength(50), Required]
    [Comment("科室名称")]
    public string DeptName { get; set; }

    /// <summary>
    /// 医生性别
    /// </summary>
    [StringLength(20)]
    [Comment("医生性别")]
    public string Sex { get; set; }

    /// <summary>
    /// 医生职称
    /// </summary>
    [StringLength(50), Required]
    [Comment("医生职称")]
    public string DoctorTitle { get; set; }

    /// <summary>
    /// 联系电话
    /// </summary>
    [StringLength(20)]
    [Comment("联系电话")]
    public string Telephone { get; set; }

    /// <summary>
    /// 医生擅长
    /// </summary>
    [StringLength(50)]
    [Comment("医生擅长")]
    public string Skill { get; set; }

    /// <summary>
    /// 医生简介
    /// </summary>
    [StringLength(500)]
    [Comment("医生简介")]
    public string Introdution { get; set; }

    /// <summary>
    /// 麻醉处方权限
    /// </summary>
    [Comment("麻醉处方权限")]
    public bool AnaesthesiaAuthority { get; set; }

    /// <summary>
    /// 处方权限
    /// </summary>
    [Comment("处方权限")]
    public bool DrugAuthority { get; set; }

    /// <summary>
    /// 精神处方权限
    /// </summary>
    [Comment("精神处方权限")]
    public bool SpiritAuthority { get; set; }

    /// <summary>
    /// 抗生素处方权限
    /// </summary>
    [Comment("抗生素处方权限")]
    public bool AntibioticAuthority { get; set; }

    /// <summary>
    /// 医师执业代码
    /// </summary>
    [StringLength(20), Required]
    [Comment("医师执业代码")]
    public string PracticeCode { get; set; }

    /// <summary>
    /// 是否启用
    /// </summary>
    [Comment("是否启用")]
    public bool IsActive { get; set; }
    /// <summary>
    /// 人员类型		1.急诊医生  2.急诊护士 0.其他人员
    /// </summary>
    [Comment("人员类型	1.急诊医生  2.急诊护士 0.其他人员")]
    public int DoctorType { get; set; }
}