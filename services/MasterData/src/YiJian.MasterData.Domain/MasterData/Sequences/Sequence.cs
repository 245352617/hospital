using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS;

namespace YiJian.MasterData.Sequences;


/// <summary>
/// 序列
/// </summary>
[Comment("序列")]
public class Sequence : FullAuditedAggregateRoot<int>
{
    /// <summary>
    /// 编码
    /// </summary>
    [Required]
    [StringLength(20)]
    [Comment("编码")]
    public string Code { get; private set; }

    /// <summary>
    /// 名称
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("名称")]
    public string Name { get; private set; }

    /// <summary>
    /// 序列值
    /// </summary>
    [Comment("序列值")]
    public int Value { get; private set; }

    /// <summary>
    /// 格式
    /// </summary>
    [StringLength(20)]
    [Comment("格式")]
    public string Format { get; private set; }

    /// <summary>
    /// 序列值长度
    /// </summary>
    [Comment("序列值长度")]
    public int Length { get; private set; }

    /// <summary>
    /// 日期
    /// </summary>
    [Column(TypeName = "date")]
    [Comment("日期")]
    public DateTime Date { get; private set; }

    /// <summary>
    /// 备注
    /// </summary>
    [StringLength(200)]
    [Comment("备注")]
    public string Memo { get; private set; }

    #region constructor

    /// <summary>
    /// 编码构造器
    /// </summary>
    /// <param name="code">编码</param>
    /// <param name="name">名称</param>
    /// <param name="value">序列值</param>
    /// <param name="format">格式</param>
    /// <param name="length">序列值长度</param>
    /// <param name="date">日期</param>
    /// <param name="memo">备注</param>
    public Sequence([NotNull] string code,// 编码
        [NotNull] string name,        // 名称
        int value,                    // 序列值
        string format,                // 格式
        int length,                   // 序列值长度
        DateTime date,                // 日期
        string memo                   // 备注
        )
    {
        //编码
        Code = Check.NotNull(code, "编码", SequenceConsts.MaxCodeLength);

        Modify(name,   // 名称
            value,          // 序列值
            format,         // 格式
            length,         // 序列值长度
            date,           // 日期
            memo            // 备注
            );
    }

    #endregion constructor

    #region Modify

    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="name">名称</param>
    /// <param name="value">序列值</param>
    /// <param name="format">格式</param>
    /// <param name="length">序列值长度</param>
    /// <param name="date">日期</param>
    /// <param name="memo">备注</param>
    public void Modify([NotNull] string name,// 名称
        int value,                    // 序列值
        string format,                // 格式
        int length,                   // 序列值长度
        DateTime date,                // 日期
        string memo                   // 备注
        )
    {
        //名称
        Name = Check.NotNull(name, "名称", SequenceConsts.MaxNameLength);

        //序列值
        Value = value;

        //格式
        Format = Check.Length(format, "格式", SequenceConsts.MaxFormatLength);

        //序列值长度
        Length = length;

        //日期
        Date = date;

        //备注
        Memo = Check.Length(memo, "备注", SequenceConsts.MaxMemoLength);
    }

    #endregion Modify

    #region constructor

    private Sequence()
    {
        // for EFCore
    }

    #endregion constructor

    #region Increase

    /// <summary>
    /// 递增
    /// </summary>
    public void Increase()
    {
        if (!Format.IsNullOrEmpty() && Format.Contains("YYYYMMDD") && (DateTime.Today - Date).TotalDays != 0)
        {
            Date = DateTime.Today;
            Value = 1;
            return;
        }

        Value++;
    }

    #endregion Increase

    #region GetResult

    /// <summary>
    /// 结果
    /// </summary>
    /// <returns></returns>
    public string GetResult()
    {
        if (Format == "YYYYMMDD")
        {
            return Date.ToString("yyyyMMdd") + Value.ToString().PadLeft(Length, '0');
        }

        return Value.ToString().PadLeft(Length, '0');
    }

    #endregion GetResult

    #region GetResult

    /// <summary>
    /// 结果
    /// </summary>
    /// <returns></returns>
    public IEnumerable<string> GetMutilResult(int count)
    {
        for (int i = 0; i < count; i++)
        {
            Increase();
            yield return GetResult();
        }
    }

    #endregion GetResult
}