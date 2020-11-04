using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace CM.ChampagneApp.Instrumentation.Http
{
    public class MonitoringHttpHandler : DelegatingHandler
    {
        private readonly IEventCollector collector;

        public MonitoringHttpHandler(HttpMessageHandler innerHandler, IEventCollector collector) : base(innerHandler)
        {
            this.collector = collector;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var sw = Stopwatch.StartNew();
            var result = await base.SendAsync(request, cancellationToken);
            sw.Stop();
            collector.TrackHttpCall(request.Method.Method, request.RequestUri.ToString(), (int)result.StatusCode, TimeSpan.FromMilliseconds(sw.ElapsedMilliseconds));

            return result;
        }
    }
}
