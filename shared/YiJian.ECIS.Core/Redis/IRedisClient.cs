using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace YiJian.ECIS.Core.Redis
{
    public interface IRedisClient
    {
        Task<T> SetRedisDataAsync<T>(string key, T data, TimeSpan? expiry = null);

        Task<T> GetRedisDataAsync<T>(string key, Func<Task<T>> action, TimeSpan? expiry = null);

        Task RedisSetStringAsync(string key, string value);

        Task<string> RedisGetStringAsync(string key);

        #region List
        Task RedisListLeftPushAsync(string key, string value);

        Task RedisListRightPushAsync(string key, string value);

        Task<string> RedisListLeftPopAsync(string key);

        Task<string> RedisListRightPopAsync(string key);

        Task<int> RedisListLengthAsync(string key);

        Task<string> RedisGetListByIndexAsync(string key, int index);
        #endregion

        #region Hash
        Task RedisHashSetAsync(string key, string field, string value);

        Task<string> RedisHashGetAsync(string key, string field);

        Task<string> RedisHashValuesAsync(string key);

        Task<int> RedisHashLengthAsync(string key);

        Task<bool> RedisHashIsExistedAsync(string key, string field);

        #endregion

        /// <summary>
        /// 获取递增数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<long> IncrementAsync(string key);

        Task<bool> RedisIsExistedAsync(string key);

        Task RedisSetExpireTimeAsync(string key, TimeSpan ts);

        Task RedisDeleteAsync(string key);

        IDatabase Database();


    }
}
