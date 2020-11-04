using System;
using System.Collections.Generic;

using Xamarin.Forms;
using CM.ChampagneApp.DTO.Models.GETModels.BrandModels;

namespace CM.ChampagneApp.UI.Pages.InfoPages.BrandInfoPage
{
    public partial class BrandInfoPage : BaseContentPage
    {
        public BrandInfoPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }
    }
}
