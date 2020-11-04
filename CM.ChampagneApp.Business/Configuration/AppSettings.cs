using System;
using Microsoft.AppCenter;

namespace CM.ChampagneApp.Business.Configuration
{
    public class AppSettings
    {
        public EnvironmentName Environment { get; set; }
        public Uri ConfigurationEndpoint { get; set; }
        public string ApplicationId { get; set; }
        public string BuildId { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public AppCenterSection AppCenter { get; set; }
        public FallbackSection Fallback { get; set; }
        
        public class FallbackSection {
            public Uri ApiUrl { get; set; }
            public Uri BlobUrl { get; set; }
            public Uri IdpUrl { get; set; }
        }

        public class AppCenterSection
        {
            public bool EnableAppCenterIntegration { get; set; } = true;
            public string AppSecret { get; set; }
            public bool EnableAnalytics { get; set; }
            public bool EnableCrashes { get; set; }
            public bool EnableDistribute { get; set; }
            public bool EnablePush { get; set; }
            public LogLevel LogLevel { get; set; } = LogLevel.Info;
        }
    }
}
