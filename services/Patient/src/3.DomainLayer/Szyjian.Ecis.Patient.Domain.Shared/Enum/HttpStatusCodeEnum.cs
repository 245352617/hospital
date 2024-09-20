using System.ComponentModel;

namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public enum HttpStatusCodeEnum
    {
        [Description("成功")]
        Ok = 200,
        [Description("失败")]
        Error = 400,
        [Description("未授权")]
        UnAuthorized = 401,
        [Description("禁止访问，无权限")]
        Forbidden = 403,
        [Description("访问的资源不存在")]
        NotFound = 404,
        [Description("服务报错啦")]
        InternalError = 500
    }
}
