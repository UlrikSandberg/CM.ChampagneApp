using System;
using System.Collections.Generic;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.CustomEventArgs
{
    public class ChampagneClickedEventArgs
    {

        public UIChampagneRoot SelectedChampagne { get; private set; }

        public ChampagneClickedEventArgs(UIChampagneRoot champagne)
        {
            this.SelectedChampagne = champagne;
        }
    }
}
