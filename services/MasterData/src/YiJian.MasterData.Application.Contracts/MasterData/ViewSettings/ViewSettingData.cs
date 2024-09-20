using System;
using System.Collections.Generic;

namespace YiJian.MasterData.ViewSettings;

/// <summary>
/// 视图配置 读取输出
/// </summary>
[Serializable]
public class ViewSettingData
{
    public int Id { get; set; }
    /// <summary>
    /// 属性
    /// </summary>
    public string Prop { get; set; }

    /// <summary>
    /// 默认标头
    /// </summary>
    public string DefaultLabel { get; set; }

    /// <summary>
    /// 标头
    /// </summary>
    public string Label { get; set; }

    /// <summary>
    /// 默认标头对齐
    /// </summary>
    public string DefaultHeaderAlign { get; set; }

    /// <summary>
    /// 标头对齐
    /// </summary>
    public string HeaderAlign { get; set; }

    /// <summary>
    /// 默认对齐
    /// </summary>
    public string DefaultAlign { get; set; }

    /// <summary>
    /// 对齐
    /// </summary>
    public string Align { get; set; }

    /// <summary>
    /// 默认宽度
    /// </summary>
    public int DefaultWidth { get; set; }

    /// <summary>
    /// 宽度
    /// </summary>
    public int Width { get; set; }

    /// <summary>
    /// 默认最小宽度
    /// </summary>
    public int DefaultMinWidth { get; set; }

    /// <summary>
    /// 最小宽度
    /// </summary>
    public int MinWidth { get; set; }

    /// <summary>
    /// 默认显示
    /// </summary>
    public bool DefaultVisible { get; set; }

    /// <summary>
    /// 是否显示
    /// </summary>
    public bool Visible { get; set; }

    /// <summary>
    /// 默认是否提示
    /// </summary>
    public bool DefaultShowTooltip { get; set; }

    /// <summary>
    /// 是否提示
    /// </summary>
    public bool ShowTooltip { get; set; }

    /// <summary>
    /// 默认序号
    /// </summary>
    public int DefaultIndex { get; set; }

    /// <summary>
    /// 序号
    /// </summary>
    public int Index { get; set; }

    /// <summary>
    /// 视图
    /// </summary>
    public string View { get; set; }

    /// <summary>
    /// 注释
    /// </summary>
    public string Comment { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public int ParentID { get; set; }

    public List<ViewSettingData> Children { get; set; }

}
