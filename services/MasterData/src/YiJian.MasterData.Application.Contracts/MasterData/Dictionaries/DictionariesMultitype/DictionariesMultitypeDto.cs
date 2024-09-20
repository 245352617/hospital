using System;
using System.Collections.Generic;

namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型 读取输出
/// </summary>
[Serializable]
public class DictionariesMultitypeDto
{
    public Guid Id { get; set; }

    /// <summary>
    ///分组编码
    /// </summary>
    public string GroupCode { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    public string GroupName { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 配置类型:  boolRadio =0 布尔单选,radio=1 多值单选 ，checkBox=2 多选，dropDownList=3 下拉框
    /// </summary>
    public int Type { get; set; }

    /// <summary>
    /// 配置值
    /// </summary>
    public string Value { get; set; }

    /// <summary>
    /// 数据来源，0：急诊添加，1：预检分诊同步
    /// </summary>
    public int DataFrom { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }

}

/// <summary>
/// 字典多类型
/// </summary>
public class DictionariesMultitypeGroupDto
{
    /// <summary>
    ///分组编码
    /// </summary>
    public string GroupCode { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary>
    public string GroupName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<DictionariesMultitypeDto> dictionariesMultitypeDtos { get; set; } = new List<DictionariesMultitypeDto>();
}