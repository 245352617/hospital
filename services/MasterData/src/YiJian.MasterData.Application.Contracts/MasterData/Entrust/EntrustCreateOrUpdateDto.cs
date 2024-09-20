using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.MasterData;

/// <summary>
/// 嘱托新增或修改
/// </summary>
public class EntrustCreateOrUpdateDto
{
    /// <summary>
    /// id ，修改必传，新增不用
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 嘱托名称
    /// </summary>
    [Required(ErrorMessage = "嘱托名称必填！")]
    public string Name { get; set; }

    /// <summary>
    /// 医嘱类型编码
    /// </summary>
    [Required(ErrorMessage = "医嘱类型编码必填！")]
    public string PrescribeTypeCode { get; set; }

    /// <summary>
    /// 医嘱类型：临嘱、长嘱、出院带药等
    /// </summary>
    [Required(ErrorMessage = "医嘱类型名称必填！")]
    public string PrescribeTypeName { get; set; }

    /// <summary>
    /// 领量(数量)
    /// </summary>
    [Required(ErrorMessage = "数量必填！")]
    public int RecieveQty { get; set; }

    /// <summary>
    /// 领量单位
    /// </summary>
    [StringLength(20)]
    [Required(ErrorMessage = "单位必填！")]
    public string RecieveUnit { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }
}