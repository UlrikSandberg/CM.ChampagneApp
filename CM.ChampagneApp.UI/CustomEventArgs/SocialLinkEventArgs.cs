using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class SocialLinkEventArgs
    {

        public string URL { get; private set; }

        public SocialLinkEventArgs(string URL)
        {
            this.URL = URL;
        }
    }
}
