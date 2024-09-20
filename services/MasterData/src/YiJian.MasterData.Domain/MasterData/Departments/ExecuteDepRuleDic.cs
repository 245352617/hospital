using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.MasterData.Departments;

/// <summary>
/// 执行科室规则字典
/// </summary>
[Comment("执行科室规则字典")]
public class ExecuteDepRuleDic : Entity<int>
{
    /// <summary>
    /// 规则id
    /// </summary>
    [Comment("规则id")]
    [Required]
    public int RuleId { get; set; }

    /// <summary>
    /// 规则名称
    /// </summary>
    [Comment("规则名称")]
    [Required, StringLength(100, ErrorMessage = "规则名称太长，请保存长度在100个字符内")]
    public string RuleName { get; set; }

    /// <summary>
    /// 执行科室代码
    /// </summary>
    [Comment("执行科室代码")]
    [Required, StringLength(100, ErrorMessage = "执行科室代码太长，请保存长度在100个字符内")]
    public string ExeDepCode { get; set; }

    /// <summary>
    /// 执行科室名称
    /// </summary>
    [Comment("执行科室名称")]
    [Required, StringLength(200, ErrorMessage = "执行科室名称太长，请保存长度在200个字符内")]
    public string ExeDepName { get; set; }

    /// <summary>
    /// 拼英编号
    /// </summary>
    [Comment("拼英编号")]
    [Required, StringLength(100, ErrorMessage = "拼英编号太长，请保存长度在100个字符内")]
    public string SpellCode { get; set; }

    /// <summary>
    /// 对比两个规则是否一致，一致返回true,否则返回false
    /// </summary>
    /// <param name="newEnity"></param>
    /// <returns></returns>
    public bool DiffAndEqual(ExecuteDepRuleDic newEnity)
    {
        if (RuleId != newEnity.RuleId || RuleName != newEnity.RuleName 
            || ExeDepCode != newEnity.ExeDepCode || ExeDepName != newEnity.ExeDepName
            || SpellCode != newEnity.SpellCode)
        {
            return false;
        }
        return true;
    }

    /// <summary>
    /// equals
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public override bool Equals(object obj)
    {
        if (obj is ExecuteDepRuleDic)
        {
            ExecuteDepRuleDic dic = obj as ExecuteDepRuleDic;
            return RuleId == dic.RuleId 
                && RuleName == dic.RuleName
                && ExeDepCode == dic.ExeDepCode 
                && ExeDepName == dic.ExeDepName
                && SpellCode == dic.SpellCode;
        }
        return false;
    }

    /// <summary>
    /// 重写哈希值
    /// </summary>
    /// <returns></returns>
    public override int GetHashCode()
    {
        return RuleId.GetHashCode() ^ RuleName.GetHashCode() ^ ExeDepCode.GetHashCode() ^ ExeDepName.GetHashCode() ^ SpellCode.GetHashCode();
    }
     
}
