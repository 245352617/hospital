using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.MasterData;

/// <summary>
/// 嘱托配置
/// </summary>
[Comment("嘱托配置")]
public class Entrust : Entity<Guid>
{
    /// <summary>
    /// 构造
    /// </summary>
    /// <param name="id"></param>
    /// <param name="code">编码</param>
    /// <param name="name">名称</param>
    /// <param name="prescribeTypeCode"></param>
    /// <param name="prescribeTypeName"></param>
    /// <param name="frequencyCode"></param>
    /// <param name="frequencyName"></param>
    /// <param name="recieveQty"></param>
    /// <param name="recieveUnit"></param>
    /// <param name="sort"></param>
    public Entrust(Guid id, string code, string name, string prescribeTypeCode, string prescribeTypeName,
        string frequencyCode, string frequencyName, int recieveQty, string recieveUnit, int sort) : base(id)
    {
        Code = code;
        FrequencyCode = frequencyCode;
        FrequencyName = frequencyName;
        Modify(name, prescribeTypeCode, prescribeTypeName,
            recieveQty, recieveUnit, sort);
    }

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name"></param>
    /// <param name="prescribeTypeCode"></param>
    /// <param name="prescribeTypeName"></param>
    /// <param name="recieveQty"></param>
    /// <param name="recieveUnit"></param>
    /// <param name="sort"></param>
    public void Modify(string name, string prescribeTypeCode, string prescribeTypeName,
        int recieveQty, string recieveUnit, int sort)
    {
        Name = name;
        PrescribeTypeCode = prescribeTypeCode;
        PrescribeTypeName = prescribeTypeName;
        RecieveQty = recieveQty;
        RecieveUnit = recieveUnit;
        Sort = sort;
        PyCode = name.FirstLetterPY();
        WbCode = name.FirstLetterWB();
    }

    /// <summary>
    /// 修改排序
    /// </summary>
    /// <param name="sort"></param>
    public void ModifySort(int sort)
    {
        Sort = sort;
    }

    /// <summary>
    /// 嘱托编码
    /// </summary>
    [Comment("嘱托编码")]
    [Required, StringLength(50)]
    public string Code { get; private set; }

    /// <summary>
    /// 嘱托名称
    /// </summary>
    [Comment("嘱托名称")]
    [Required, StringLength(100)]
    public string Name { get; private set; }

    /// <summary>
    /// 医嘱类型编码
    /// </summary>
    [Comment("医嘱类型编码")]
    [Required, StringLength(20)]
    public string PrescribeTypeCode { get; private set; }

    /// <summary>
    /// 医嘱类型：临嘱、长嘱、出院带药等
    /// </summary>
    [Comment("医嘱类型：临嘱、长嘱、出院带药等")]
    [Required, StringLength(20)]
    public string PrescribeTypeName { get; private set; }

    /// <summary>
    /// 频次编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("频次编码")]
    public string FrequencyCode { get; private set; }

    /// <summary>
    /// 频次名称
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("频次名称")]
    public string FrequencyName { get; private set; }

    /// <summary>
    /// 领量(数量)
    /// </summary>
    [Comment("领量(数量)")]
    public int RecieveQty { get; private set; }

    /// <summary>
    /// 领量单位
    /// </summary>
    [Comment("领量单位")]
    [StringLength(20)]
    public string RecieveUnit { get; private set; }

    /// <summary>
    /// 拼音码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("拼音码")]
    public string PyCode { get; private set; }

    /// <summary>
    /// 五笔码
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("五笔码")]
    public string WbCode { get; private set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Required]
    [Comment("排序")]
    public int Sort { get; private set; }
}