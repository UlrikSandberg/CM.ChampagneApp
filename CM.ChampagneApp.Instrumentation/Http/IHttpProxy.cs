using System;
using System.Net.Http;

namespace CM.ChampagneApp.Instrumentation.Http
{
    public interface IHttpProxy
    {
        HttpClient HttpClient { get; } 
    }
}
