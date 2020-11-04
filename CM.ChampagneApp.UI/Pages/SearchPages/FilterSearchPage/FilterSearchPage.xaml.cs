using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.SearchPages.FilterSearchPage
{
    public partial class FilterSearchPage : ContentPage
    {
        public FilterSearchPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            var ViewModel = (FilterSearchPageModel)this.BindingContext;
            if(ViewModel.NavigateBack.CanExecute(null))
            {
                ViewModel.NavigateBack.Execute(null);
            }

        }
    }
}
