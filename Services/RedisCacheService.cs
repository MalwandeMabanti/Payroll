namespace Payroll.Services
{
    using System;
    using System.Collections.Generic;

    using Microsoft.Extensions.Caching.Distributed;

    using Newtonsoft.Json;

    using Payroll.Interfaces;

    public class RedisCacheService : ICacheService
    {
        private readonly IDistributedCache redis;

        public RedisCacheService(IDistributedCache redis)
        {
            this.redis = redis;
        }

        public List<T> GetData<T>(string key)
        {
            var data = this.redis.GetString(key);

            if (data == null)
            {
                return null;
            }

            return JsonConvert.DeserializeObject<List<T>>(data);
        }

        public void SaveData<T>(string key, List<T> data, TimeSpan timeSpan)
        {
            var serializedData = JsonConvert.SerializeObject(data);

            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = timeSpan,
            };

            this.redis.SetString(key, serializedData, options);
        }
    }
}
