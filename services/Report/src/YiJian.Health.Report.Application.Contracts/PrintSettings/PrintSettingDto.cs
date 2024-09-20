using System;

namespace YiJian.Health.Report.PrintSettings;

/// <summary>
/// 打印配置
/// </summary>
public class PrintSettingDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 目录Id
    /// </summary>
    public Guid CataLogId { get; set; }
    /// <summary>
    /// 编码
    /// </summary>
    public string Code { get; set; }
    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 传参Url
    /// </summary>
    public string ParamUrl { get; set; }

    /// <summary>
    /// 排序
    /// </summary>
    public int Sort { get; set; }

    /// <summary>
    /// 状态
    /// </summary>
    public bool Status { get; set; }

    /// <summary>
    /// 模板类型（FastReport, DevExpress, Html）
    /// </summary>
    public string TempType { get; set; }

    /// <summary>
    /// 打印方式（Web, CLodop, SzyjTray）
    /// </summary>
    public string PrintMethod { get; set; }

    /// <summary>
    /// 模板内容
    /// </summary>
    public string TempContent { get; set; }
    /// <summary>
    /// 纸张编码
    /// </summary>
    public string PageSizeCode { get; set; }

    /// <summary>
    /// 纸张高度
    /// </summary>
    public decimal PageSizeHeight { get; set; }

    /// <summary>
    /// 纸张宽度
    /// </summary>
    public string PageSizeWidth { get; set; }

    /// <summary>
    /// 布局
    /// </summary>
    public string Layout { get; set; }

    /// <summary>
    /// 是否预览
    /// </summary>
    public bool IsPreview { get; set; }
    /// <summary>
    /// 单据类型编码
    /// </summary>
    public string Comm { get; set; }
    /// <summary>
    /// 备注
    /// </summary>
    public string Remark { get; set; }
}