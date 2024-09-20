using System;

namespace YiJian.MasterData.VitalSign;

/// <summary>
/// 生命体征表达式 新增输入
/// </summary>
public class CreateOrUpdateVitalSignExpressionDto
{
    public Guid Id { get; set; }
    /// <summary>
    /// 评分项
    /// </summary>
    public string ItemName { get; set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    public string StLevelExpression { get; set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    public string NdLevelExpression { get; set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    public string RdLevelExpression { get; set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    public string ThALevelExpression { get; set; }

    /// <summary>
    /// Ⅰ级评分表达式
    /// </summary>
    public string ThBLevelExpression { get; set; }
}