using System;

namespace YiJian.Health.Report.PrintSettings;

/// <summary>
/// 打印配置模板内容
/// </summary>
public class PrintSettingFrxDto
{
    /// <summary>
    /// 主键Id
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// 名称
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// 模板内容
    /// </summary>
    public string TempContent { get; set; }
}
