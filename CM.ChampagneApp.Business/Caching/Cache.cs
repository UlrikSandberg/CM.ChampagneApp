using System;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Configuration;
using Microsoft.AppCenter.Crashes;
using MonkeyCache.LiteDB;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace CM.ChampagneApp.Business.Caching
{
    public class Cache : ICache
    {
        public Cache()
        {
            Barrel.ApplicationId = AppConfiguration.LocalAppSettings.ApplicationId;
        }

        public void Add<T>(string key, T data, TimeSpan expireIn, string eTag = null, JsonSerializerSettings jsonSerializationSettings = null)
        {
            Barrel.Current.Add<T>(key, data, expireIn, eTag, jsonSerializationSettings);
        }

        public void Empty(params string[] key)
        {
            Barrel.Current.Empty(key);
        }

        public void EmptyAll()
        {
            Barrel.Current.EmptyAll();
        }

        public void EmptyExpired()
        {
            Barrel.Current.EmptyExpired();
        }

        public bool Exists(string key)
        {
            return Barrel.Current.Exists(key);
        }

        public T Get<T>(string key, JsonSerializerSettings jsonSettings = null)
        {
            return Barrel.Current.Get<T>(key, jsonSettings);
        }

        public string GetETag(string key)
        {
            return Barrel.Current.GetETag(key);
        }

        public DateTime? GetExpiration(string key)
        {
            return Barrel.Current.GetExpiration(key);
        }

        public T GetOrCreateObject<T>(string key, Func<T> create, TimeSpan expireIn, bool forceRefresh = false)
        {
            if (Exists(key) && !IsExpired(key) && !forceRefresh)
                return Get<T>(key);

            var obj = create();
            Add(key, obj, expireIn);

            return obj;
        }

        public async Task<T> GetOrFetchObject<T>(string key, Func<Task<T>> fetch, TimeSpan expireIn, bool forceRefresh = false)
        {
            try
            {
                if ((!Exists(key) || IsExpired(key) || forceRefresh) && Connectivity.NetworkAccess.HasFlag(NetworkAccess.Internet))
                {
                    var obj = await fetch();
                    Add(key, obj, expireIn);

                    return obj;
                }
            } catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            return Exists(key) ? Get<T>(key) : default(T);
        }

        public bool IsExpired(string key)
        {
            return Barrel.Current.IsExpired(key);
        }
    }
}
