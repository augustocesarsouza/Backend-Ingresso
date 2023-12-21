using Ingresso.Infra.Data.UtilityExternal.Interface;
using Microsoft.Extensions.Caching.Distributed;

namespace Ingresso.Infra.Data.UtilityExternal
{
    public class CacheRedisUti : ICacheRedisUti
    {
        private readonly IDistributedCache _distributedCache;

        public CacheRedisUti(IDistributedCache distributedCache)
        {
            _distributedCache = distributedCache;
        }

        public Task<string> GetStringAsyncWrapper(string key, CancellationToken token = default)
        {
            return _distributedCache.GetStringAsync(key, token);
        }

        public void RemoveWrapper(string key)
        {
            _distributedCache.Remove(key);
        }

        public Task SetStringAsyncWrapper(string key, string value, CancellationToken token = default)
        {
            var returnTask = _distributedCache.SetStringAsync(key, value, token);
            return returnTask;
        }

        public Task SetStringAsyncWrapper(string key, string value, DistributedCacheEntryOptions options, CancellationToken token = default)
        {
            var returnTask = _distributedCache.SetStringAsync(key, value, options, token);
            return returnTask;
        }
    }
}
