using System;
using Xamarin.Forms;

namespace CM.ChampagneApp.UI.Pages.SharedPages.WebViewPage
{
    public partial class WebViewPage : ContentPage
    {
        public WebViewPage()
        {
			NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();
        }

        void Handle_GoForwardRequested(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Go forward requested!");
        }

        void Handle_GoBackRequested(object sender, System.EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Go back requested!");
        }

        void Handle_Navigating(object sender, Xamarin.Forms.WebNavigatingEventArgs e)
        {
            LoadingLabel.IsVisible = true;
        }

        void Handle_Navigated(object sender, Xamarin.Forms.WebNavigatedEventArgs e)
        {
            LoadingLabel.IsVisible = false;
        }

        void Handle_OnLeftNavIconClicked(object sender, System.EventArgs e)
        {
            if (Browser.CanGoBack)
            {
                Browser.GoBack();
            }
            else
            {
                var ViewModel = (WebViewPageModel)this.BindingContext;
                if (ViewModel.NavigateBack.CanExecute(null))
                {
                    ViewModel.NavigateBack.Execute(null);
                }
            }
        }

        void Handle_OnRightNavIconClicked(object sender, System.EventArgs e)
        {
            if (Browser.CanGoForward)
            {
                Browser.GoForward();
            }
        }
    }
}
