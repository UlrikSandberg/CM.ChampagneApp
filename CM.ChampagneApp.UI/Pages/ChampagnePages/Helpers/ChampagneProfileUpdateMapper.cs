using System;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models;
using CM.ChampagneApp.UI.UIFacade.Models.UIChampagneModels;

namespace CM.ChampagneApp.UI.Pages.ChampagnePages.Helpers
{
    public class ChampagneProfileUpdateMapper : IUpdateMapper<UIChampagne, UIChampagne>
    {   
        public void UpdateData(UIChampagne data, UIChampagne update)
        {
            data.RatingSumOfTastings = update.RatingSumOfTastings;
            data.NumberOfTastings = update.NumberOfTastings;
            data.IsTastedByRequester = update.IsTastedByRequester;
            data.IsBookmarkedByRequester = update.IsBookmarkedByRequester;
            data.RequesterTasting = update.RequesterTasting;
        }
    }
}
