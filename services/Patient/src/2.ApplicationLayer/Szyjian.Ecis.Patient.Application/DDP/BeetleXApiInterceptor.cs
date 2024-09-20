using BeetleX.Http.Clients;
using Castle.DynamicProxy;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace YiJian.ECIS.DDP
{
    /// <summary>
    /// 描    述:CallApi日志拦截器
    /// 创 建 人:杨凯
    /// 创建时间:2023/12/8 17:14:21
    /// </summary>
    public class BeetleXApiInterceptor<T> : IInterceptor
    {
        private readonly ILogger<T> _logger;

        public BeetleXApiInterceptor(ILogger<T> logger)
        {
            _logger = logger;
        }

        public void Intercept(IInvocation invocation)
        {
            MethodInfo methodInfo = invocation.Method;
            PostAttribute postAttr = methodInfo.GetCustomAttribute<PostAttribute>();
            string url = string.Empty;
            if (postAttr != null)
            {
                url = postAttr.Route;
            }

            object[] param = invocation.Arguments;
            _logger.LogInformation("请求CallApi的URL:{URL} 请求的参数:{@Param)}", url, param);

            invocation.Proceed();

            object result = invocation.ReturnValue;
            _logger.LogInformation("请求CallApi的URL:{URL} CallApi返回的结果:{@Param}", url, result);
        }
    }
}
