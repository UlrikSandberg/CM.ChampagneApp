using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.InfoPages.ChampagneInfoPage
{
    public partial class ChampagneInfoPage : ContentPage
    {
        public ChampagneInfoPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (ChampagneInfoPageModel)this.BindingContext;
            if(ViewModel.NavigateBack.CanExecute(null))
            {
                ViewModel.NavigateBack.Execute(null);
            }
        }
    }
}
