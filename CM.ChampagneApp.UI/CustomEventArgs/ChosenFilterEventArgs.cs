using System;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ChosenFilterEventArgs
    {
        public bool Toggled { get; private set; }
        public FilterSearchTag Filter { get; private set; }

        public ChosenFilterEventArgs(FilterSearchTag filter, bool toggled)
        {
            Toggled = toggled;
            Filter = filter;
        }
    }
}
