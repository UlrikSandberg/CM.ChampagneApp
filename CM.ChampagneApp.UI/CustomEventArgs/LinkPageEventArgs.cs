using System;
namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class LinkPageEventArgs
    {
       
        public string SelectedPageLink { get; set; }

        public LinkPageEventArgs(string URLPath)
        {
            this.SelectedPageLink = URLPath;
        }

    }
}
