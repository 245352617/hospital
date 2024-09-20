using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace YiJian.BodyParts
{
   
    /// <summary>
    /// 请求用户令牌参数验证
    /// </summary>
    public class TokenAttribute : ActionFilterAttribute
    {
        private const string tokenHeader = "Authorization";

        /// <summary>
        /// 执行拦截操作
        /// </summary>
        /// <param name="filterContext"></param>
        public async override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                //判断是否忽略权限认证
                var countAttribute = ((ControllerActionDescriptor)filterContext.ActionDescriptor)?.MethodInfo?.GetCustomAttributes(typeof(AllowAnonymousAttribute)).Count();
                if (countAttribute > 0) {
                    return;
                }
                //验证是否包含TOKEN
                var hasAuthHeader = filterContext.HttpContext.Request.Headers.Keys.Contains(tokenHeader);
                if (hasAuthHeader)
                {
                    return;
                }
                else
                {
                    filterContext.HttpContext.Response.ContentType = "application/json;charset=utf-8";

                    filterContext.HttpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;

                    var result = new JsonResult { Code = 401, Msg = "未能获取到登录身份信息，请检查后重试 (×_×)", Data = "UnAuthorized,ActionTokenFilter" };

                    await filterContext.HttpContext.Response.WriteAsync(result.ToString());
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
            }          
        }
    }

}