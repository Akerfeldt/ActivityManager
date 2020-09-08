using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using ActivityManager.Data;
using ActivityManager.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;

namespace ActivityManager.Services.Caches
{
    public class LookupsCache : ILookupsCache
    {
        private readonly IDistributedCache _cache;
        private readonly ApplicationDbContext _applicationDbContext;

        public LookupsCache(ApplicationDbContext applicationDbContext, IDistributedCache distributedCache)
        {
            _applicationDbContext = applicationDbContext;
            _cache = distributedCache;
        }

        public async Task<IDictionary<int, Activity>> GetActivities(bool fromSource = false)
        {
            const string key = "activities";

            var cacheItem = await GetAsync<IDictionary<string, Activity>>(key);
            if (fromSource || cacheItem == null)
            {
                cacheItem = await _applicationDbContext.Activities.AsNoTracking().ToDictionaryAsync(x => x.Id.ToString(), x => new Activity { Id = x.Id, Description = x.Description });
                await SetAsync(key, cacheItem);
            }

            return cacheItem.ToDictionary(x => x.Value.Id, x => x.Value);
        }

        public Task Prime()
        {
            return Task.WhenAll(GetActivities(true));
        }

        private async Task<T> GetAsync<T>(string key)
        {
            var json = await _cache.GetStringAsync(key);

            if (json == null) return default;

            return JsonSerializer.Deserialize<T>(json);
        }

        private async Task SetAsync(string key, object foo)
        {
            var json = JsonSerializer.Serialize(foo);

            await _cache.SetStringAsync(key, json);
        }
    }
}
