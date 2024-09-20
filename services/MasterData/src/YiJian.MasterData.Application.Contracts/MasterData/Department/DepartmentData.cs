using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.MasterData;

[Serializable]
public class DepartmentData 
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    /// <summary>
    /// 挂号科室编码
    /// </summary>
    public string RegisterCode { get; set; }

    public bool IsActived { get; set; }
}

/// <summary>
/// 执行科室规则字典
/// </summary>
public class ExecuteDepRuleDicDto
{
    /// <summary>
    /// 规则id
    /// </summary> 
    public int RuleId { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary> 
    public string RuleName { get; set; }

    /// <summary>
    /// 执行科室代码
    /// </summary> 
    public string ExeDepCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary> 
    public string ExeDepName { get; set; }

    /// <summary>
    /// 拼英编号
    /// </summary> 
    public string SpellCode { get; set; }

}
