using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CM.ChampagneApp.Instrumentation
{
    public interface IEventCollector
    {
        void TrackEvent(string name, IDictionary<string, string> additionalProperties = null);
        void TrackPageView(string pageName);
        void TrackIntention(string actionName);
        void TrackHttpCall(string verb, string url, int statusCode, TimeSpan duration);
    }
}
