namespace YiJian.MasterData;

/// <summary>
/// 查询His药品信息入参
/// </summary>
public class MedicinesHisInput
{
    /// <summary>
    /// 药品名称 为空则查询所有药品信
    /// </summary>
    public string DrugsName { get; set; }

    /// <summary>
    /// 药房类型 0代表查询所有药房药品,对应字典信息：2.1药房编码码字典（字典、写死）
    /// </summary>
    public string PharmacyType { get; set; }

    /// <summary>
    /// 开始页码
    /// </summary>
    public int PageIndex { get; set; }

    /// <summary>
    /// 每页数据
    /// </summary>
    public int PageSize { get; set; }
}