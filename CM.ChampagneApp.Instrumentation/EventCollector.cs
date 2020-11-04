using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AppCenter.Analytics;

namespace CM.ChampagneApp.Instrumentation
{
    public class EventCollector : IEventCollector
    {

        public void TrackEvent(string name, IDictionary<string, string> additionalProperties = null)
        {
            Analytics.TrackEvent(name, additionalProperties);
        }

        public void TrackHttpCall(string verb, string url, int statusCode, TimeSpan duration)
        {
            Analytics.TrackEvent($"HttpCall", new Dictionary<string, string>() 
            { 
                { "verb", verb }, 
                { "url", url },
                { "statuscode", statusCode.ToString() },
                { "duration", duration.Milliseconds.ToString() } 
            });
        }

        public void TrackIntention(string actionName)
        {
            Analytics.TrackEvent($"Intention.{actionName}");
        }

        public void TrackPageView(string pageName)
        {
            Analytics.TrackEvent($"PageView.{pageName}");
        }
    }
}
