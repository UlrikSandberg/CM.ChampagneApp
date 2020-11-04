using System;
using System.Net;
using System.Net.Http;
using ModernHttpClient;

namespace CM.ChampagneApp.Instrumentation.Http
{
    public class HttpProxy : IHttpProxy
    {
        public HttpClient HttpClient { get; }

        public HttpProxy(IEventCollector eventCollector)
        {
            HttpClient = new HttpClient(new MonitoringHttpHandler(new NativeMessageHandler(), eventCollector));
        }
    }
}
