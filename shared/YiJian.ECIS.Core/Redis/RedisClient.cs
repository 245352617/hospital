using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace YiJian.ECIS.Core.Redis
{
    /// <summary>
    /// Redis操作类
    /// </summary>
    public class RedisClient : IRedisClient
    {
        private readonly IDatabase _redis;

        private readonly RedisOptions _options;

        public RedisClient(RedisHelper redisHelper, IOptions<RedisOptions> optionAccessor)
        {
            if (optionAccessor == null)
            {
                throw new ArgumentNullException("optionAccessor");
            }

            _options = optionAccessor.Value;
            _redis = redisHelper.GetDatabase();
        }

        /// <summary>
        /// 根据key获取Redis数据
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="action"></param>
        /// <param name="expiry"></param>
        /// <returns></returns>
        public async Task<T> GetRedisDataAsync<T>(string key, Func<Task<T>> action, TimeSpan? expiry = null)
        {
            try
            {
                RedisValue redisValue = await _redis.StringGetAsync(key);
                if (!redisValue.IsNull)
                {
                    return JsonConvert.DeserializeObject<T>(redisValue);
                }
                else if (action != null)
                {
                    T data = await action.Invoke();
                    if (data != null)
                    {
                        await _redis.StringSetAsync(key, JsonConvert.SerializeObject(data), expiry);
                    }
                    return data;
                }

                return default;
            }
            catch
            {
                return await action.Invoke();
            }
        }


        public async Task<T> SetRedisDataAsync<T>(string key, T data, TimeSpan? expiry = null)
        {
            try
            {
                if (data != null)
                {
                    await _redis.StringSetAsync(key, JsonConvert.SerializeObject(data), expiry);
                }
                return data;
            }
            catch
            {
                return data;
            }
        }

        /// <summary>
        /// Redis根据key设置value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public virtual async Task RedisSetStringAsync(string key, string value)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.StringSetAsync(key, value);
        }

        /// <summary>
        /// Redis根据key获取value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<string> RedisGetStringAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.StringGetAsync(key);
        }

        #region List

        public virtual async Task RedisListLeftPushAsync(string key, string value)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.ListLeftPushAsync(key, value);
        }

        public virtual async Task RedisListRightPushAsync(string key, string value)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.ListRightPushAsync(key, value);
        }

        public virtual async Task<string> RedisListLeftPopAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.ListLeftPopAsync(key);
        }

        public virtual async Task<string> RedisListRightPopAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.ListLeftPopAsync(key);
        }

        public virtual async Task<int> RedisListLengthAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return Convert.ToInt32(await _redis.ListLengthAsync(key));
        }

        public virtual async Task<string> RedisGetListByIndexAsync(string key, int index)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.ListGetByIndexAsync(key, index);
        }
        #endregion


        #region Hash

        public virtual async Task RedisHashSetAsync(string key, string field, string value)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.HashSetAsync(key, field, value);
        }

        public virtual async Task<string> RedisHashGetAsync(string key, string field)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.HashGetAsync(key, field);
        }

        public virtual async Task<string> RedisHashValuesAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            string text = string.Join(',', await _redis.HashValuesAsync(key));
            return "[" + text + "]";
        }

        public virtual async Task<int> RedisHashLengthAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return Convert.ToInt32(await _redis.HashLengthAsync(key));
        }

        public virtual async Task<bool> RedisHashIsExistedAsync(string key, string field)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.HashExistsAsync(key, field);
        }

        #endregion

        /// <summary>
        /// 获取递增数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public virtual async Task<long> IncrementAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.StringIncrementAsync(key);
        }

        public virtual async Task<bool> RedisIsExistedAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            return await _redis.KeyExistsAsync(key);
        }

        public virtual async Task RedisSetExpireTimeAsync(string key, TimeSpan ts)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.KeyExpireAsync(key, ts);
        }

        public virtual async Task RedisDeleteAsync(string key)
        {
            key = _options.DefaultKeyPrefix + key;
            await _redis.KeyDeleteAsync(key);
        }

        public virtual IDatabase Database()
        {
            return _redis;
        }
    }
}