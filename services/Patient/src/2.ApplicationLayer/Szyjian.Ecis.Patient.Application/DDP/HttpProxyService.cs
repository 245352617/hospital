using BeetleX.Http.Clients;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace YiJian.ECIS.DDP
{
    /// <summary>
    /// DDP API 客户端
    /// </summary>
    public class HttpProxyService : IScopedDependency
    {
        private readonly ILogger<HttpProxyService> _log;

        public HttpProxyService(ILogger<HttpProxyService> logger)
        {
            _log = logger;
        }

        /// <summary>
        /// 构建代理服务
        /// </summary>
        /// <returns></returns>
        public T BuildAgent<T>(string url) where T : class
        {
            using HttpCluster httpCluster = new HttpCluster();
            httpCluster.TimeOut = 60 * 1000;
            httpCluster.DefaultNode.Add(url);
            T service = httpCluster.Create<T>();

            //ProxyGenerator proxyGenerator = new ProxyGenerator();
            //T proxyService = proxyGenerator.CreateInterfaceProxyWithTarget(service, new BeetleXApiInterceptor<HttpProxyService>(_log));

            return service;
        }
    }
}
