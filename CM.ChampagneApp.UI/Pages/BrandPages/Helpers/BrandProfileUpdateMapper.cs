using System;
using CM.ChampagneApp.UI.Pages.Commons.Helpers;
using CM.ChampagneApp.UI.UIFacade.Models;


namespace CM.ChampagneApp.UI.Pages.BrandPages.Helpers
{
    public class BrandProfileUpdateMapper : IUpdateMapper<UIBrandProfile,UIBrandProfile>
    {
        public void UpdateData(UIBrandProfile data, UIBrandProfile update)
        {
            data.NumberOfFollowers = update.NumberOfFollowers;
            data.IsRequesterFollowing = update.IsRequesterFollowing;
            data.NumberOfTastings = update.NumberOfTastings;
        }
    }
}