
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Validation;

namespace YiJian.MasterData.ViewSettings;


/// <summary>
/// 视图配置 新增输入
/// </summary>
[Serializable]
public class ViewSettingCreation
{
    /// <summary>
    /// 属性
    /// </summary>
    [Required(ErrorMessage = "属性不能为空！")]
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxPropLength), ErrorMessage = "属性最大长度不能超过{1}!")]
    public string Prop { get; set; }

    /// <summary>
    /// 默认标头
    /// </summary>
    [Required(ErrorMessage = "默认标头不能为空！")]
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxDefaultLabelLength), ErrorMessage = "默认标头最大长度不能超过{1}!")]
    public string DefaultLabel { get; set; }

    /// <summary>
    /// 标头
    /// </summary>
    [Required(ErrorMessage = "标头不能为空！")]
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxLabelLength), ErrorMessage = "标头最大长度不能超过{1}!")]
    public string Label { get; set; }

    /// <summary>
    /// 默认标头对齐
    /// </summary>
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxDefaultHeaderAlignLength), ErrorMessage = "默认标头对齐最大长度不能超过{1}!")]
    public string DefaultHeaderAlign { get; set; }

    /// <summary>
    /// 标头对齐
    /// </summary>
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxHeaderAlignLength), ErrorMessage = "标头对齐最大长度不能超过{1}!")]
    public string HeaderAlign { get; set; }

    /// <summary>
    /// 默认对齐
    /// </summary>
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxDefaultAlignLength), ErrorMessage = "默认对齐最大长度不能超过{1}!")]
    public string DefaultAlign { get; set; }

    /// <summary>
    /// 对齐
    /// </summary>
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxAlignLength), ErrorMessage = "对齐最大长度不能超过{1}!")]
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
    [Required(ErrorMessage = "视图不能为空！")]
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxViewLength), ErrorMessage = "视图最大长度不能超过{1}!")]
    public string View { get; set; }

    /// <summary>
    /// 注释
    /// </summary>
    [Required(ErrorMessage = "注释不能为空！")]
    [DynamicStringLength(typeof(ViewSettingConsts), nameof(ViewSettingConsts.MaxCommentLength), ErrorMessage = "注释最大长度不能超过{1}!")]
    public string Comment { get; set; }

    /// <summary>
    /// 父级ID
    /// </summary>
    public int ParentID { get; set; }
}
