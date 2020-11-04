using System;

namespace CM.ChampagneApp.UI.Pages.Top10Pages.Top10LightroomPage.Helpers
{
    public class Top10LightroomInitData
    {
        public string ConfigurationKey { get; }
        public bool IsVintage { get; }
        public string Title { get; }

        public Top10LightroomInitData(string configurationKey, bool isVintage, string title)
        {
            ConfigurationKey = configurationKey;
            IsVintage = isVintage;
            Title = title;
        }

        public string Subtitle
        {
            get
            {
                return IsVintage ? "Top 10 Vintage" : "Top 10 Non-Vintage";
            }
        }
    }
}
