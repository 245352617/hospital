using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YiJian.MasterData;

/// <summary>
/// 手术字典
/// </summary>
[Comment("手术字典")]
public class Operation : BaseEntity<Guid>
{

    /// <summary>
    /// 手术编码
    /// </summary> 
    [StringLength(50)]
    [Comment("手术编码")]
    public string OperationCode { get; set; }

    /// <summary>
    /// 手术名称
    /// </summary>
    //[Column(TypeName = "nvarchar(500)")]
    [StringLength(500, ErrorMessage = "手术名称最大长度500字符内")]
    [Comment("手术名称")]
    public string OperationName { get; set; }

    /// <summary>
    /// 拼音编码
    /// </summary> 
    [StringLength(50)]
    [Comment("拼音编码")]
    public string PyCode { get; set; }

    /// <summary>
    /// 时长
    /// </summary>
    [Comment("时长")]
    public int Duration { get; set; }

    /// <summary>
    /// 价格
    /// </summary>
    [Comment("价格")]
    public decimal Price { get; set; }
}