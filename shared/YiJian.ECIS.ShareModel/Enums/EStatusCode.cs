using System.ComponentModel;

namespace YiJian.ECIS.ShareModel.Enums;

/// <summary>
/// 状态码
/// </summary>
public enum EStatusCode
{
    #region 常用状态码

    /// <summary>
    /// 失败
    /// </summary>
    [Description("失败")]
    CFail = -1,

    /// <summary>
    /// 缺省默认成功
    /// </summary>
    [Description("正常")]
    CNormal = 0,

    /// <summary>
    /// 确认成功
    /// </summary>
    [Description("成功")]
    COK = 1,

    /// <summary>
    /// 无数据
    /// </summary>
    [Description("无数据")]
    CNULL = 2,

    /// <summary>
    /// 参数丢失
    /// </summary>
    [Description("参数丢失")]
    ParameterIsMissing = 3,

    /// <summary>
    /// 数据存在
    /// </summary>
    [Description("数据存在")]
    CExist = 4,

    /// <summary>
    /// 警告
    /// </summary>
    [Description("警告")]
    CWarn = 5,

    #endregion

    #region 100-600 http 状态码

    //消息

    /// <summary>
    /// Continue
    /// </summary>
    [Description("Continue")]
    C100 = 100,

    /// <summary>
    /// Switching Protocols
    /// </summary>
    [Description("Switching Protocols")]
    C101 = 101,
    /// <summary>
    /// Processing
    /// </summary>
    [Description("Processing")]
    C102 = 102,

    //成功 

    /// <summary>
    /// OK
    /// </summary>
    [Description("OK")]
    C200 = 200,

    /// <summary>
    /// Created
    /// </summary>
    [Description("Created")]
    C201 = 201,

    /// <summary>
    /// Accepted
    /// </summary>
    [Description("Accepted")]
    C202 = 202,

    /// <summary>
    /// Non-Authoritative Information
    /// </summary>
    [Description("Non-Authoritative Information")]
    C203 = 203,

    /// <summary>
    /// No Content
    /// </summary>
    [Description("No Content")]
    C204 = 204,

    /// <summary>
    /// Reset Content
    /// </summary>
    [Description("Reset Content")]
    C205 = 205,

    /// <summary>
    /// Partial Content
    /// </summary>
    [Description("Partial Content")]
    C206 = 206,

    /// <summary>
    /// Multi-Status
    /// </summary>
    [Description("Multi-Status")]
    C207 = 207,

    //重定向 

    /// <summary>
    /// Multiple Choices
    /// </summary>
    [Description("Multiple Choices")]
    C300 = 300,

    /// <summary>
    /// Moved Permanently
    /// </summary>
    [Description("Moved Permanently")]
    C301 = 301,

    /// <summary>
    /// Move Temporarily
    /// </summary>
    [Description("Move Temporarily")]
    C302 = 302,

    /// <summary>
    /// See Other
    /// </summary>
    [Description("See Other")]
    C303 = 303,

    /// <summary>
    /// Not Modified
    /// </summary>
    [Description("Not Modified")]
    C304 = 304,

    /// <summary>
    /// Use Proxy
    /// </summary>
    [Description("Use Proxy")]
    C305 = 305,

    /// <summary>
    /// Switch Proxy
    /// </summary>
    [Description("Switch Proxy")]
    C306 = 306,

    /// <summary>
    /// Temporary Redirect
    /// </summary>
    [Description("Temporary Redirect")]
    C307 = 307,

    //请求错误

    /// <summary>
    /// Bad Request
    /// </summary>
    [Description("Bad Request")]
    C400 = 400,

    /// <summary>
    /// Unauthorized
    /// </summary>
    [Description("Unauthorized")]
    C401 = 401,

    /// <summary>
    /// Payment Required
    /// </summary>
    [Description("Payment Required")]
    C402 = 402,

    /// <summary>
    /// Forbidden
    /// </summary>
    [Description("Forbidden")]
    C403 = 403,

    /// <summary>
    /// Not Found
    /// </summary>
    [Description("Not Found")]
    C404 = 404,

    /// <summary>
    /// Method Not Allowed
    /// </summary>
    [Description("Method Not Allowed")]
    C405 = 405,

    /// <summary>
    /// Not Acceptable
    /// </summary>
    [Description("Not Acceptable")]
    C406 = 406,

    /// <summary>
    /// Proxy Authentication Required
    /// </summary>
    [Description("Proxy Authentication Required")]
    C407 = 407,

    /// <summary>
    /// Request Timeout
    /// </summary>
    [Description("Request Timeout")]
    C408 = 408,

    /// <summary>
    /// Conflict
    /// </summary>
    [Description("Conflict")]
    C409 = 409,

    /// <summary>
    /// Gone
    /// </summary>
    [Description("Gone")]
    C410 = 410,

    /// <summary>
    /// Length Required
    /// </summary>
    [Description("Length Required")]
    C411 = 411,

    /// <summary>
    /// Precondition Failed
    /// </summary>
    [Description("Precondition Failed")]
    C412 = 412,

    /// <summary>
    /// Request Entity Too Large
    /// </summary>
    [Description("Request Entity Too Large")]
    C413 = 413,

    /// <summary>
    /// Request-URI Too Long
    /// </summary>
    [Description("Request-URI Too Long")]
    C414 = 414,

    /// <summary>
    /// Unsupported Media Type
    /// </summary>
    [Description("Unsupported Media Type")]
    C415 = 415,

    /// <summary>
    /// Requested Range Not Satisfiable
    /// </summary>
    [Description("Requested Range Not Satisfiable")]
    C416 = 416,

    /// <summary>
    /// Expectation Failed
    /// </summary>
    [Description("Expectation Failed")]
    C417 = 417,

    /// <summary>
    /// I'm a teapot
    /// </summary>
    [Description("I'm a teapot")]
    C418 = 418,

    /// <summary>
    /// Misdirected Request
    /// </summary>
    [Description("Misdirected Request")]
    C421 = 421,

    /// <summary>
    /// Unprocessable Entity
    /// </summary>
    [Description("Unprocessable Entity")]
    C422 = 422,

    /// <summary>
    /// Locked
    /// </summary>
    [Description("Locked")]
    C423 = 423,

    /// <summary>
    /// Failed Dependency
    /// </summary>
    [Description("Failed Dependency")]
    C424 = 424,

    /// <summary>
    /// Too Early
    /// </summary>
    [Description("Too Early")]
    C425 = 425,

    /// <summary>
    /// Upgrade Required
    /// </summary>
    [Description("Upgrade Required")]
    C426 = 426,

    /// <summary>
    /// Retry With
    /// </summary>
    [Description("Retry With")]
    C449 = 449,

    /// <summary>
    /// Unavailable For Legal Reasons
    /// </summary>
    [Description("Unavailable For Legal Reasons")]
    C451 = 451,

    //服务器错误

    /// <summary>
    /// Internal Server Error
    /// </summary>
    [Description("Internal Server Error")]
    C500 = 500,

    /// <summary>
    /// Not Implemented
    /// </summary>
    [Description("Not Implemented")]
    C501 = 501,

    /// <summary>
    /// Bad Gateway
    /// </summary>
    [Description("Bad Gateway")]
    C502 = 502,

    /// <summary>
    /// Service Unavailable
    /// </summary>
    [Description("Service Unavailable")]
    C503 = 503,

    /// <summary>
    /// Gateway Timeout
    /// </summary>
    [Description("Gateway Timeout")]
    C504 = 504,

    /// <summary>
    /// HTTP Version Not Supported
    /// </summary>
    [Description("HTTP Version Not Supported")]
    C505 = 505,

    /// <summary>
    /// Variant Also Negotiates
    /// </summary>
    [Description("Variant Also Negotiates")]
    C506 = 506,

    /// <summary>
    /// Insufficient Storage
    /// </summary>
    [Description("Insufficient Storage")]
    C507 = 507,

    /// <summary>
    /// Bandwidth Limit Exceeded
    /// </summary>
    [Description("Bandwidth Limit Exceeded")]
    C509 = 509,

    /// <summary>
    /// Not Extended
    /// </summary>
    [Description("Not Extended")]
    C510 = 510,

    /// <summary>
    /// Unparseable Response Headers
    /// </summary>
    [Description("Unparseable Response Headers")]
    C600 = 600,

    #endregion

    #region 10000-90000 业务状态码

    /// <summary>
    /// 存在引用关系，不能删除
    /// </summary>
    [Description("存在引用关系，不能删除")]
    C10000 = 10000,

    /// <summary>
    /// 文件关联丢失，文件无法和目录对应上
    /// </summary>
    [Description("文件关联丢失,文件无法和目录对应上")]
    C10001 = 10001,


    #endregion

}
