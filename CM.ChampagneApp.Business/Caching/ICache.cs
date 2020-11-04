using System;
using System.Threading.Tasks;
using MonkeyCache;

namespace CM.ChampagneApp.Business.Caching
{
    public interface ICache : IBarrel
    {
        Task<T> GetOrFetchObject<T>(string key, Func<Task<T>> fetch, TimeSpan expireIn, bool forceRefresh = false);
        T GetOrCreateObject<T>(string key, Func<T> create, TimeSpan expireIn, bool forceRefresh = false);
    }
}
