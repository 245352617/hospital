using System.ComponentModel;


namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 患者服务定义的Http返回状态
/// </summary>
public enum EHttpStatusCodeEnum
{
    /// <summary>
    /// 成功
    /// </summary>
    [Description("成功")]
    Ok = 200,
    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    Error = 400,
    /// <summary>
    /// 未授权
    /// </summary>
    [Description("未授权")]
    UnAuthorized = 401,
    /// <summary>
    /// 禁止访问，无权限
    /// </summary>
    [Description("禁止访问，无权限")]
    Forbidden = 403,
    /// <summary>
    /// 访问的资源不存在
    /// </summary>
    [Description("访问的资源不存在")]
    NotFound = 404,
    /// <summary>
    /// 服务报错啦
    /// </summary>
    [Description("服务报错啦")]
    InternalError = 500
}
