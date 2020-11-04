using System;
namespace CM.ChampagneApp.UI.Pages.SharedPages.Helpers
{
    public class WebViewInitData
    {
        public string WebUrl { get; private set; }

        public WebViewInitData(string url)
        {
            WebUrl = url;
        }
    }
}
