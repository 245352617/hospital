using System;

namespace YiJian.MasterData;

/// <summary>
/// 嘱托Dto
/// </summary>
public class EntrustDto
{
    /// <summary>
    /// Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 嘱托编码
    /// </summary>
    public string Code { get; set; }

    /// <summary>
    /// 嘱托名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 医嘱类型编码
    /// </summary>
    public string PrescribeTypeCode { get; set; }

    /// <summary>
    /// 医嘱类型：临嘱、长嘱、出院带药等
    /// </summary>
    public string PrescribeTypeName { get; set; }

    /// <summary>
    /// 频次编码
    /// </summary>
    public string FrequencyCode { get; set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    public string FrequencyName { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public string ProjectType { get; set; } = "9";

    /// <summary>
    /// 
    /// </summary>
    public string ProjectName { get; set; } = "嘱托";

    /// <summary>
    /// 领量(数量)
    /// </summary>
    public int RecieveQty { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary>
    public string RecieveUnit { get; set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    public string PyCode { get; set; }

    /// <summary>
    /// 五笔码
    /// </summary>
    public string WbCode { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
}