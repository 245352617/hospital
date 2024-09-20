using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.MasterData.DictionariesMultitypes;

/// <summary>
/// 字典多类型
/// </summary>
[Comment("字典多类型")]
public class DictionariesMultitype : FullAuditedAggregateRoot<Guid>
{
    /// <summary>
    /// 分组编码
    /// </summary>
    [Column(TypeName = "nvarchar(50)")]
    [Comment("字典分组编码")]
    public string GroupCode { get; set; }

    /// <summary>
    /// 分组名称
    /// </summary> 
    [StringLength(100)]
    [Comment("字典分组名称")]
    public string GroupName { get; set; }

    /// <summary>
    /// 字典编码
    /// </summary> 
    [Required]
    [StringLength(50)]
    [Comment("字典编码")]
    public string Code { get; set; }

    /// <summary>
    /// 字典名称
    /// </summary> 
    [StringLength(100)]
    [Comment("字典名称")]
    public string Name { get; set; }

    /// <summary>
    /// 配置类型
    /// </summary>
    [Required]
    [Comment("配置类型")]
    public int Type { get; set; }

    /// <summary>
    /// 配置值
    /// </summary>
    [Required]
    [StringLength(500)]
    [Comment("配置值")]
    public string Value { get; set; }

    /// <summary>
    /// 数据来源，0：急诊添加，1：预检分诊同步
    /// </summary>
    [Comment("数据来源，0：急诊添加，1：预检分诊同步")]
    public int DataFrom { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    [Comment("状态")]
    public bool Status { get; set; }

    /// <summary>
    /// 备注
    /// </summary>
    [Comment("备注")]
    [StringLength(200)]
    public string Remark { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    [Comment("排序")]
    public int Sort { get; set; }

    #region constructor
    /// <summary>
    /// 字典类型编码构造器
    /// </summary>
    /// <param name="id"></param>
    /// <param name="groupName">组名称</param>
    /// <param name="groupCode">组编码</param>
    /// <param name="code">字典编码</param>
    /// <param name="name">字典名称</param>
    /// <param name="type">配置类型</param>
    /// <param name="value">配置值</param>
    /// <param name="remark">备注</param>
    /// <param name="dataFrom">数据来源，1：预检分诊</param>
    /// <param name="status"></param>
    /// <param name="sort"></param>
    public DictionariesMultitype(Guid id,
        string groupCode,
        string groupName,
        [NotNull] string code,// 字典类型编码
        string name,  // 字典类型名称
        [NotNull] int type,
        [NotNull] string value,
        int dataFrom = 0, //数据来源，1：预检分诊
        string remark = "",               // 备注
        bool status = true,
        int sort = 0
        ) : base(id)
    {
        //字典类型编码
        GroupCode = groupCode;
        GroupName = groupName;
        Code = Check.NotNull(code, "字典类型编码");
        Name = name;
        Type = Check.NotNull(type, "配置类型");
        Value = Check.NotNull(value, "配置值");
        DataFrom = dataFrom;
        Remark = remark;
        Status = status;
        Sort = sort;
    }
    #endregion

    #region constructor
    private DictionariesMultitype()
    {
        // for EFCore
    }
    #endregion

    #region Modify
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="newDictionariesMultitype"></param>
    public void Modify(DictionariesMultitype newDictionariesMultitype
        )
    {
        GroupCode = newDictionariesMultitype.GroupCode;
        GroupName = newDictionariesMultitype.GroupName;
        Name = newDictionariesMultitype.Name;
        Type = newDictionariesMultitype.Type;
        Value = newDictionariesMultitype.Value;
        DataFrom = newDictionariesMultitype.DataFrom;
        Status = newDictionariesMultitype.Status;
        Remark = newDictionariesMultitype.Remark;
        Sort = newDictionariesMultitype.Sort;
    }
    #endregion

}
