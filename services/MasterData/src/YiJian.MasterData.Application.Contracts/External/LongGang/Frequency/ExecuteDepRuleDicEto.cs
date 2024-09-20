using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.EventBus;

namespace YiJian.MasterData.External.LongGang.Frequency;

/// <summary>
/// 执行科室规则字典
/// </summary>
[EventName("ExecuteDepRuleDicEvents")]
public class ExecuteDepRuleDicEto
{
    /// <summary>
    /// 数据
    /// </summary>
    public List<ExecuteDepRuleDicData> DicDatas { get; set; }

    /// <summary>
    /// 字典类型:1-检验;2-检查;3-科室;4-员工;5-费别;6-诊断;7-组套指引;8-诊疗材料;9-手术;10-药品用法;11-药品频次;12-药品信息;13-检验标本;14-检查分类;15执行科室规则字典
    /// </summary>
    public int DicType { get; set; }
}


/// <summary>
/// 执行科室规则字典
/// </summary> 
public class ExecuteDepRuleDicData
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
