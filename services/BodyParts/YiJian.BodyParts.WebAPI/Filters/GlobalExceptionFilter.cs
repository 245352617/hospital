using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using Serilog;
using Volo.Abp.Authorization;
using Volo.Abp.Validation;

namespace YiJian.BodyParts.WebAPI
{
    /// <summary>
    /// 全局异常处理拦截
    /// </summary>
    public class GlobalExceptionFilter : IExceptionFilter
    {

        public GlobalExceptionFilter()
        {
        }


        public async void OnException(ExceptionContext context)
        {
            try
            {
                if (!context.ModelState.IsValid)
                {
                    string exMsg = string.Empty;
                    if (context.Exception is AbpValidationException)
                    {
                        var exlist = ((AbpValidationException)context.Exception).ValidationErrors.Select(e => e.ErrorMessage);
                        exMsg = string.Join(",", exlist);
                    }
                    await ExceptionHandlerAsync(context.HttpContext, $"模型验证错误，请检查后重试！{string.Join(", ", exMsg)}", 500);
                }
                else if (context.Exception is AbpAuthorizationException)
                {
                    await ExceptionHandlerAsync(context.HttpContext, GetStatusCodesMsg(403), 403);
                }
                //数据库异常特殊处理
                else if(context.Exception is SqlException || context.Exception?.InnerException is SqlException) {
                    SqlException tmpException = null;

                    if(context.Exception is SqlException)
                        tmpException = context.Exception as SqlException;
                    else
                        tmpException = context.Exception.InnerException as SqlException;

                    Log.Error(context.Exception, context.Exception.Message);
                    var msg = getSqlExceptionErrMsg(tmpException);
                    await ExceptionHandlerAsync(context.HttpContext, msg, 500);
                }
                else
                {
                    Log.Error(context.Exception, context.Exception.Message);
                    await ExceptionHandlerAsync(context.HttpContext, "服务器繁忙", 500);
                }
            }
            catch (Exception)
            {
                Log.Error(context.Exception, context.Exception.Message);
            }

            context.ExceptionHandled = true;
        }



        private string GetStatusCodesMsg(int statusCodes)
        {
            string msg;
            switch (statusCodes)
            {
                case StatusCodes.Status404NotFound:
                    msg = "您请求的接口或页面不存在或移除，请检查后重试！";
                    break;
                case StatusCodes.Status401Unauthorized:
                    msg = "请重新登录后，再来操作重试！";
                    break;
                case StatusCodes.Status403Forbidden:
                    msg = "您没有当前操作或请求权限，请检查后重试！";
                    break;
                case StatusCodes.Status501NotImplemented:
                    msg = "接口或方法未实现，请联系管理员！";
                    break;
                case StatusCodes.Status405MethodNotAllowed:
                    msg = "请求方式/类型出错，请检查后重试！";
                    break;
                case StatusCodes.Status415UnsupportedMediaType:
                    msg = "请求的MediaType类型出错，请检查后重试！";
                    break;
                default:
                    msg = string.Empty;
                    break;
            }
            return msg;
        }


        /// <summary>
        /// 异常处理，返回JSON
        /// </summary>
        /// <param name="context"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async Task ExceptionHandlerAsync(HttpContext context, string message, int code = -1)
        {
            if (!context.Response.HasStarted)
            {
                context.Response.ContentType = "application/json;charset=utf-8";
                if (code > -1 && code != 200) context.Response.StatusCode = code;
            }
            
            await context.Response.WriteAsync(JsonResult.Write(code, message, null).ToString());
        }

        /// <summary>
        /// 获取数据库异常的友好提示
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        private string getSqlExceptionErrMsg(SqlException ex) {
            return ex.Number switch {
                10060 => $"网络错误，连接数据库失败，请稍后重试",
                _ => "服务器繁忙"
            };
        }
    }
}
