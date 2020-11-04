using System;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using CM.ChampagneApp.Business.Caching;
using CM.ChampagneApp.DTO.Models.GETModels;
using CM.ChampagneApp.Instrumentation.Http;
using Newtonsoft.Json;

namespace CM.ChampagneApp.Business.Configuration
{
    public interface IAppConfiguration
    {
        EnvironmentName Environment { get; }
        string ClientSecret { get; }
        string ClientId { get; }
        string ApplicationId { get; }
        string BuildId { get; }
        Uri IdentityUrl { get; }
        Uri BlobStorageBaseUrl { get; }
        Uri BlobBrandStorageUrl { get; }
        Uri BlobUserStorageUrl { get; }
        Uri ApiBaseUrl { get; }
    }

    public enum EnvironmentName
    {
        LOCAL,
        QA,
        PROD
    }

    public class AppConfiguration : IAppConfiguration
    {
        private readonly ICache cache;
        private readonly IHttpProxy httpProxy;
        private static AppSettings _localAppSettings = null;
        public static AppSettings LocalAppSettings
        {
            get
            {
                if (_localAppSettings == null)
                {
                    var ass = typeof(AppConfiguration).GetTypeInfo().Assembly;
                    using (var stream = ass.GetManifestResourceStream("CM.ChampagneApp.Business.app.settings"))
                    {
                        using (StreamReader sr = new StreamReader(stream))
                        {
                            var document = sr.ReadToEnd();
                            _localAppSettings = JsonConvert.DeserializeObject<AppSettings>(document);
                        }
                    }
                }

                return _localAppSettings;
            }
        }

        public EnvironmentName Environment => LocalAppSettings.Environment;
        public string ApplicationId => LocalAppSettings.ApplicationId;
        public string BuildId => LocalAppSettings.BuildId;
        public string ClientId => LocalAppSettings.ClientId;
        public string ClientSecret => LocalAppSettings.ClientSecret;

        private static ConfigurationModel ServerAppSettings;
        public Uri IdentityUrl => ServerAppSettings.IdentityBaseUrl;
        public Uri BlobStorageBaseUrl => ServerAppSettings.BlobStorageBaseUrl;
        public Uri BlobBrandStorageUrl => new Uri(BlobStorageBaseUrl, "/brand-files/");
        public Uri BlobUserStorageUrl => new Uri(BlobStorageBaseUrl, "/user-files/");
        public Uri ApiBaseUrl => ServerAppSettings.ApiBaseUrl;

        public AppConfiguration(ICache cache, IHttpProxy httpProxy)
        {
            this.cache = cache;
            this.httpProxy = httpProxy;

            //Eager load this as soon as possible
            //LoadConfiguration(LocalAppSettings.ConfigurationEndpoint).ConfigureAwait(true).GetAwaiter().GetResult();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            //var cfg = await cache.GetOrFetchObject("ServerAppConfig", async () =>
            //{
            //    using (var client = new HttpClient())
            //    {
            //        client.DefaultRequestHeaders.Add("X-Client-ID", ClientId);
            //        client.DefaultRequestHeaders.Add("X-Client-Secret", ClientSecret);

            //        var response = await client.GetAsync(configEndpoint);
            //        response.EnsureSuccessStatusCode();

            //        return await response.Content.ReadAsAsync<ConfigurationModel>();
            //    }
            //}, TimeSpan.FromDays(7));

            ServerAppSettings = new ConfigurationModel
            {
                ApiBaseUrl = LocalAppSettings.Fallback.ApiUrl,
                BlobStorageBaseUrl = LocalAppSettings.Fallback.BlobUrl,
                IdentityBaseUrl = LocalAppSettings.Fallback.IdpUrl
            };
        }
    }
}
