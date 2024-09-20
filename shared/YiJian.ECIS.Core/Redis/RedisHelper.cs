using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;
using System.Collections.Concurrent;

namespace YiJian.ECIS.Core.Redis
{
    public class RedisHelper : IDisposable
    {
        private readonly ConcurrentDictionary<string, ConnectionMultiplexer> _concurrentDictionary;

        private readonly RedisOptions _options;

        public RedisHelper(IOptions<RedisOptions> optionsAccessor)
        {
            _concurrentDictionary = new ConcurrentDictionary<string, ConnectionMultiplexer>();
            if (optionsAccessor == null)
            {
                throw new ArgumentNullException("optionsAccessor");
            }

            _options = optionsAccessor.Value;
        }

        private ConnectionMultiplexer GetConnect()
        {
            return _concurrentDictionary.GetOrAdd(_options.InstanceName, (string p) => ConnectionMultiplexer.Connect(_options.Connection));
        }

        public IDatabase GetDatabase()
        {
            return GetConnect().GetDatabase(_options.DefaultDb);
        }

        public IServer GetServer(string configName = null, int endPointsIndex = 0)
        {
            ConfigurationOptions configurationOptions = ConfigurationOptions.Parse(_options.Connection);
            return GetConnect().GetServer(configurationOptions.EndPoints[endPointsIndex]);
        }

        public ISubscriber GetSubscriber(string configName = null)
        {
            return GetConnect().GetSubscriber();
        }

        public void Dispose()
        {
            if (_concurrentDictionary == null || _concurrentDictionary.Count <= 0)
            {
                return;
            }

            foreach (ConnectionMultiplexer value in _concurrentDictionary.Values)
            {
                value.Close();
            }
        }
    }
}