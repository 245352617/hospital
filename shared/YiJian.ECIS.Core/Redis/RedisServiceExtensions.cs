using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace YiJian.ECIS.Core.Redis
{
    public static class RedisServiceExtensions
    {
        public static void AddRedis(this IServiceCollection service, Action<RedisOptions> configOptions)
        {
            service.AddOptions();
            service.Configure(configOptions);
            service.AddSingleton<RedisHelper>();
            service.AddTransient<IRedisClient, RedisClient>();
        }

    }

}
