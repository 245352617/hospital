using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities;
using YiJian.ECIS;
using YiJian.ECIS.ShareModel.Models;

namespace YiJian.MasterData.ViewSettings;

/// <summary>
/// 视图配置
/// </summary>
[Comment("视图配置")]
public class ViewSetting : Entity<int>, IIsActive, IComparable<ViewSetting>
{
    #region Properties

    #region props

    /// <summary>
    /// 属性
    /// </summary>
    [Required]
    [StringLength(30)]
    [Comment("属性")]
    public string Prop { get; private set; }

    /// <summary>
    /// 默认标头
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("默认标头")]
    public string DefaultLabel { get; private set; }

    /// <summary>
    /// 标头
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("标头")]
    public string Label { get; private set; }
    #endregion

    #region Align

    /// <summary>
    /// 默认标头对齐
    /// </summary>
    [StringLength(50)]
    [Comment("默认标头对齐")]
    public string DefaultHeaderAlign { get; private set; } = "left";

    /// <summary>
    /// 标头对齐
    /// </summary>
    [StringLength(50)]
    [Comment("标头对齐")]
    public string HeaderAlign { get; private set; } = "left";

    /// <summary>
    /// 默认对齐
    /// </summary>
    [StringLength(50)]
    [Comment("默认对齐")]
    public string DefaultAlign { get; private set; } = "left";

    /// <summary>
    /// 对齐
    /// </summary>
    [StringLength(50)]
    [Comment("对齐")]
    public string Align { get; private set; } = "left";

    #endregion

    #region Width
    /// <summary>
    /// 默认宽度
    /// </summary>
    [Comment("默认宽度")]
    public int DefaultWidth { get; private set; } = 100;

    /// <summary>
    /// 宽度
    /// </summary>
    [Comment("宽度")]
    public int Width { get; private set; } = 100;

    /// <summary>
    /// 默认最小宽度
    /// </summary>
    [Comment("默认最小宽度")]
    public int DefaultMinWidth { get; private set; } = 100;

    /// <summary>
    /// 最小宽度
    /// </summary>
    [Comment("最小宽度")]
    public int MinWidth { get; private set; } = 100;
    #endregion

    #region Visible
    /// <summary>
    /// 默认显示
    /// </summary>
    [Comment("默认显示")]
    public bool DefaultVisible { get; private set; } = true;

    /// <summary>
    /// 是否显示
    /// </summary>
    [Comment("是否显示")]
    public bool Visible { get; private set; } = true;

    #endregion

    #region ShowTooltip
    /// <summary>
    /// 默认是否提示
    /// </summary>
    [Comment("默认是否提示")]
    public bool DefaultShowTooltip { get; private set; } = true;

    /// <summary>
    /// 是否提示
    /// </summary>
    [Comment("是否提示")]
    public bool ShowTooltip { get; private set; } = true;
    #endregion

    #region Index

    /// <summary>
    /// 默认序号
    /// </summary>
    [Comment("默认序号")]
    public int DefaultIndex { get; private set; }

    /// <summary>
    /// 序号
    /// </summary>
    [Comment("序号")]
    public int Index { get; private set; }
    #endregion

    #region View
    /// <summary>
    /// 视图
    /// </summary>
    [Required]
    [StringLength(30)]
    [Comment("视图")]
    public string View { get; private set; }

    /// <summary>
    /// 注释
    /// </summary>
    [Required]
    [StringLength(50)]
    [Comment("注释")]
    public string Comment { get; private set; }

    #endregion

    /// <summary>
    /// 是否激活
    /// </summary>
    [Comment("是否激活")]
    public bool IsActive { get; private set; } = true;

    #region relationship
    /// <summary>
    /// 子级
    /// </summary>
    [NotMapped]
    public virtual List<ViewSetting> Children { get; set; } = new List<ViewSetting>();

    /// <summary>
    /// 父级ID
    /// </summary>
    [Comment("父级ID")]
    public int ParentID { get; private set; }
    #endregion

    #endregion

    #region Reset
    public void Reset()
    {
        Align = DefaultAlign;
        HeaderAlign = DefaultHeaderAlign;
        Index = DefaultIndex;
        Label = DefaultLabel;
        MinWidth = DefaultMinWidth;
        ShowTooltip = DefaultShowTooltip;
        Visible = DefaultVisible;
        Width = DefaultWidth;
    }
    #endregion

    #region protected constructor
    protected ViewSetting()
    {
        // for EFCore
    }
    #endregion       

    #region Modify
    /// <summary>
    /// 修改
    /// </summary>
    /// <param name="label">标头</param>
    /// <param name="headerAlign">标头对齐</param>
    /// <param name="align">对齐</param>
    /// <param name="width">宽度</param>
    /// <param name="minWidth">最小宽度</param>
    /// <param name="visible">是否显示</param>
    /// <param name="showTooltip">是否提示</param>
    /// <param name="index">序号</param>
    /// <param name="parentID">父级ID</param>
    public void Modify(
        [NotNull] string label,       // 标头
        string headerAlign,           // 标头对齐
        string align,                 // 对齐
        int width,                    // 宽度
        int minWidth,                 // 最小宽度
        bool visible,                 // 是否显示
        bool showTooltip,             // 是否提示
        int index,                    // 序号
        int parentID                  // 父级ID
        )
    {
        //标头
        Label = Check.NotNull(label, "标头", ViewSettingConsts.MaxLabelLength);

        //标头对齐
        HeaderAlign = headerAlign;

        //对齐
        Align = Check.Length(align, "对齐", ViewSettingConsts.MaxAlignLength);

        //宽度
        Width = width;

        //最小宽度
        MinWidth = minWidth;

        //是否显示
        Visible = visible;

        //是否提示
        ShowTooltip = showTooltip;

        //序号
        Index = index;

        //父级ID
        ParentID = parentID;

    }

    public int CompareTo(ViewSetting other)
    {
        /*
         小于零	此对象 CompareTo 在排序顺序中位于方法所指定的对象之前。
            零  此当前实例在排序顺序中与方法参数指定的对象出现在同一位置 CompareTo 。
        大于零	此当前实例 CompareTo 位于排序顺序中由方法自变量指定的对象之后。
         */
        if (Index == 0 && other.Index > 0) return 1;

        if (Index > 0 && other.Index == 0) return -1;

        // 默认升序
        return Index.CompareTo(other.Index);
    }
    #endregion
}
