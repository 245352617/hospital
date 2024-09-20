using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace YiJian.BodyParts.WebAPI
{
    /// <summary>
    /// 全局中间件
    /// </summary>
    public class GlobalMiddleware
    {
        private readonly RequestDelegate next;

        public GlobalMiddleware(RequestDelegate next)
        {
            this.next = next;
        }


        /// <summary>
        /// Invoke
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                Log.Error(ex, ex.Message);
                await context.Response.WriteAsync(JsonResult.Write(code: 500, msg: ex.Message, data: null).ToString());
            }
        }

    }


}
