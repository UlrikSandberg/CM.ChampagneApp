using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.Pages.SearchPages.Helpers;
using CM.ChampagneApp.UI.UIReadModels;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ChosenFiltersListEventArgs
    {

        public IEnumerable<FilterSearchTag> ChosenFilters { get; private set; }

        public ChosenFiltersListEventArgs(IEnumerable<FilterSearchTag> chosenFilters)
        {
            ChosenFilters = chosenFilters;
        }
    }
}
